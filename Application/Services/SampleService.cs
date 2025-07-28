using Application.Interface;
using Application.Repository;
using Application.Request.Sample;
using Application.Response;
using Application.Response.Sample;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Application.Services
{
    public class SampleService : ISampleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
       
        public SampleService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ILocusResultService locusResultService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var samples = await _unitOfWork.Samples.GetAllAsync(null,
                    x => x.Include(s => s.TestOrder)
                         .ThenInclude(t => t.SampleMethod)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.User)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.Service)
                         .Include(s => s.Collector));
                samples = samples.OrderByDescending(s => s.CreatedDate).ToList();
                var result = _mapper.Map<IEnumerable<SampleResponse>>(samples);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var sample = await _unitOfWork.Samples.GetAsync(s => s.Id == id,
                    x => x.Include(s => s.TestOrder)
                         .ThenInclude(t => t.SampleMethod)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.User)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.Service)
                         .Include(s => s.Collector));
                if (sample == null) return response.SetNotFound("Sample not found");
                var result = _mapper.Map<SampleResponse>(sample);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(CreateSamplesRequest requests)
        {
            var response = new ApiResponse();
            try
            {
                // 1. Lấy thông tin TestOrder cùng SampleMethod và User
                var testOrder = await _unitOfWork.TestOrders.GetAsync(
                    x => x.Id == requests.TestOrderId,
                    x => x.Include(o => o.SampleMethod)
                          .Include(o => o.User)
                );
                if (testOrder == null)
                    return response.SetBadRequest("TestOrder not found");

                // 2. Đếm số sample đã tồn tại để đảm bảo không vượt quá 2
                var existingSamples = await _unitOfWork.Samples
                    .GetAllAsync(s => s.TestOrderId == requests.TestOrderId);
                var existingCount = existingSamples.Count;
              
                var now = DateTime.UtcNow;
                var datePart = now.ToString("yyMMdd");         // ví dụ "250724"
                var orderCode = testOrder.Id.ToString("D6");    // zero-pad 6 chữ số

                // 4. Xử lý từng SampleRequest
                var claim = _claimService.GetUserClaim();
                for (int i = 0; i < requests.Participants.Count; i++)
                {
                    var req = requests.Participants[i];

                    var sample = _mapper.Map<Sample>(req);
                    sample.TestOrderId = requests.TestOrderId;
                    sample.SampleMethodId = testOrder.SampleMethodId;
                    sample.CreatedDate = now;

                    // 4.3. Sinh SampleCode duy nhất
                    var seq = existingCount + i + 1;
                    sample.SampleCode = $"ADN{orderCode}-{datePart}-{seq}";

                    // 4.4. Xác định CollectedBy
                    var methodName = testOrder.SampleMethod.Name?.ToLower() ?? "";
                    sample.CollectedBy = (methodName.Contains("self") || methodName.Contains("tự"))
                        ? testOrder.UserId
                        : claim.Id;

                    sample.ShippingProvider = requests.ShippingProvider;
                    sample.TrackingNumber = requests.TrackingNumber;
                    await _unitOfWork.Samples.AddAsync(sample);
                }

                await _unitOfWork.SaveChangeAsync();
                return response.SetOk("Create Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }


        public async Task<ApiResponse> UpdateAsync(UpdateSampleRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existing = await _unitOfWork.Samples.GetAsync(s => s.Id == request.Id);
                if (existing == null) return response.SetNotFound("Sample not found");

                // Validate TestOrder exists and include SampleMethod
                var testOrder = await _unitOfWork.TestOrders.GetAsync(x => x.Id == request.TestOrderId,
                    x => x.Include(s => s.SampleMethod)
                         .Include(s => s.User));
                if (testOrder == null)
                    return response.SetBadRequest("TestOrder not found");


                _mapper.Map(request, existing);
                existing.SampleMethodId = testOrder.SampleMethodId; // Set SampleMethodId from TestOrder
                await _unitOfWork.SaveChangeAsync();
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
                var existing = await _unitOfWork.Samples.GetAsync(s => s.Id == id);
                if (existing == null) return response.SetNotFound("Sample not found");

                await _unitOfWork.Samples.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk();
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetByTestOrderIdAsync(int testOrderId)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var testOrder = await _unitOfWork.TestOrders.GetAsync(t => t.Id == testOrderId);
                if (testOrder == null) return response.SetNotFound("TestOrder not found");

                var samples = await _unitOfWork.Samples.GetAllAsync(s => s.TestOrderId == testOrderId,
                    x => x.Include(s => s.TestOrder)
                         .ThenInclude(t => t.SampleMethod)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.User)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.Service)
                         .Include(s => s.Collector));
                var result = _mapper.Map<IEnumerable<SampleResponse>>(samples);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetByCollectorIdAsync(int collectorId)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var collector = await _unitOfWork.UserAccounts.GetAsync(u => u.Id == collectorId);
                if (collector == null) return response.SetNotFound("Collector not found");

                var samples = await _unitOfWork.Samples.GetAllAsync(s => s.CollectedBy == collectorId,
                    x => x.Include(s => s.TestOrder)
                         .ThenInclude(t => t.SampleMethod)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.User)
                         .Include(s => s.TestOrder)
                         .ThenInclude(t => t.Service)
                         .Include(s => s.Collector));
                var result = _mapper.Map<IEnumerable<SampleResponse>>(samples);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
} 