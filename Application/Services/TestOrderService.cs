using Application.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity;
using Application.Interface;
using Application.Request.TestOrder;
using Application.Response.TestOrder;
using Org.BouncyCastle.Ocsp;
using DocumentFormat.OpenXml.Drawing;

namespace Application.Services
{
    public class TestOrderService : ITestOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly IEmailService _emailService;
        public TestOrderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _emailService = emailService;
        }

       

        public async Task<ApiResponse> GetAllAsync(SearchTestOrderRequest req)
        {
            try
            {
                var items = await _unitOfWork.TestOrders.SearchTestOrdersAsync(req);
                var total = await _unitOfWork.TestOrders.CountTestOrdersAsync(req);

                var dtos = _mapper.Map<IEnumerable<TestOrderResponse>>(items);
                var paged = new PagedResult<TestOrderResponse>(dtos, total, req.PageIndex, req.PageSize);
                return new ApiResponse().SetOk(paged);
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                    .SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }



        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var order = await _unitOfWork.TestOrders.GetAsync(x => x.Id == id,
                    x => x.Include(s => s.SampleMethod)
                         .Include(s => s.Service)
                         .ThenInclude(s => s.ServiceSampleMethods)
                                .ThenInclude(ssm => ssm.SampleMethod)
                         .Include(x => x.User)
                         .Include(x => x.AppointmentStaff)
                         .Include(x => x.Samples)
                             .ThenInclude(s => s.LocusResults)
                         .Include(s => s.Result));

                if (order == null)
                {
                    return response.SetNotFound($"Test order with ID {id} not found");
                }

                var result = _mapper.Map<TestOrderResponse>(order);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(CreateTestOrderRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var service = await _unitOfWork.Services.GetAsync(x => x.Id == request.ServiceId);
                if (service == null)
                {
                    return response.SetNotFound($"Test order with service Id {request.ServiceId} not found");
                }
                var sampleMethod = await _unitOfWork.SampleMethods.GetAsync(x => x.Id == request.SampleMethodId);
                if (sampleMethod == null)
                {
                    return response.SetNotFound($"Test order with sample method Id {request.SampleMethodId} not found");
                }
                var serviceSampleMethod = await _unitOfWork.ServiceSampleMethods.GetAsync(x => x.ServiceId == request.ServiceId && x.SampleMethodId == request.SampleMethodId);

                if (serviceSampleMethod == null)
                {
                    return response.SetBadRequest("Phương pháp lấy mẫu không được hỗ trợ bởi dịch vụ này.");
                }

                var claim = _claimService.GetUserClaim();
                var order = _mapper.Map<TestOrder>(request);
                order.UserId = claim.Id;
                order.CreatedDate = DateTime.UtcNow;
                order.Status = TestOrderStatus.Pending;
                order.DeliveryKitStatus = DeliveryKitStatus.NotSent;


                await _unitOfWork.TestOrders.AddAsync(order);
                await _unitOfWork.SaveChangeAsync();

                var result = _mapper.Map<TestOrderResponse>(order);
                return response.SetOk("Create Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(UpdateTestOrderRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existingOrder = await _unitOfWork.TestOrders.GetAsync(x => x.Id == request.Id);
                if (existingOrder == null)
                {
                    return response.SetNotFound($"Test order with ID {request.Id} not found");
                }

              

                _mapper.Map(request, existingOrder);
                existingOrder.ModifiedDate = DateTime.UtcNow;

                
                await _unitOfWork.SaveChangeAsync();

                var result = _mapper.Map<TestOrderResponse>(existingOrder);
                return response.SetOk("Update Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var order = await _unitOfWork.TestOrders.GetAsync(x => x.Id == id);
                if (order == null)
                {
                    return response.SetNotFound($"Test order with ID {id} not found");
                }

                await _unitOfWork.TestOrders.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return response.SetOk("Test order deleted successfully");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateStatusAsync(UpdateTestOrderStatusRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var order = await _unitOfWork.TestOrders.GetAsync(x => x.Id == request.Id);
                if (order == null)
                {
                    return response.SetNotFound($"Test order with ID {request.Id} not found");
                }

                order.Status = request.TestOrderStatus;
                order.ModifiedDate = DateTime.UtcNow;

              
                await _unitOfWork.SaveChangeAsync();

                //var result = _mapper.Map<TestOrderResponse>(order);
                return response.SetOk("Update Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateDeliveryKitStatusAsync(UpdateDeliveryKitStatusRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var order = await _unitOfWork.TestOrders.GetAsync(x => x.Id == request.Id, x => x.Include(o => o.User));
                if (order == null)
                {
                    return response.SetNotFound($"Test order with ID {request.Id} not found");
                }

                order.DeliveryKitStatus = request.DeliveryKitStatus;
                order.ModifiedDate = DateTime.UtcNow;

                if (request.DeliveryKitStatus == DeliveryKitStatus.Sent)
                {
                    order.KitSendDate = DateTime.UtcNow;
                    var email = order.User?.Email ?? order.Email;
                    if (!string.IsNullOrEmpty(email))
                    {
                        string content = $"Kit test  cho {order.SampleMethod}đang được gửi đến địa chỉ {order.AppointmentLocation} " +
                            $"Đây là video hướng dẫn sử dụng bộ Kit lấy mẫu ADN chuyên dụng  https://www.youtube.com/watch?v=XXMbYKxf5Ks&ab_channel=VIETGEN.";
                        await _emailService.SendKitStatusEmail(email, content);
                    }
                }

                await _unitOfWork.SaveChangeAsync();

                var result = _mapper.Map<TestOrderResponse>(order);
                return response.SetOk("Update Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetByCurrentCustomerAsync()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var claim = _claimService.GetUserClaim();
                var orders = await _unitOfWork.TestOrders.GetAllAsync(x => x.UserId == claim.Id,
                    q => q.Include(s => s.SampleMethod)
                          .Include(s => s.Service)
                          .Include(x => x.AppointmentStaff)
                          .Include(x => x.Samples)
                              .Include(s => s.Result));
                orders = orders.OrderByDescending(o => o.CreatedDate).ToList();
                var result = _mapper.Map<IEnumerable<TestOrderResponse>>(orders);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
}
