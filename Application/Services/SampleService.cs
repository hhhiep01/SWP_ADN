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

namespace Application.Services
{
    public class SampleService : ISampleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public SampleService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
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
                         .Include(s => s.Collector)
                         .Include(s => s.Result)) ;
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
                         .Include(s => s.Collector)
                         .Include(s => s.Result));
                if (sample == null) return response.SetNotFound("Sample not found");
                var result = _mapper.Map<SampleResponse>(sample);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(SampleRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var claim = _claimService.GetUserClaim();
                // Validate TestOrder exists and include SampleMethod
                var testOrder = await _unitOfWork.TestOrders.GetAsync(x => x.Id == request.TestOrderId,
                    x => x.Include(s => s.SampleMethod)
                         .Include(s => s.User));
                if (testOrder == null)
                    return response.SetBadRequest("TestOrder not found");
                var sample = _mapper.Map<Sample>(request);
                // Set CollectedBy based on SampleMethod from TestOrder
                if (testOrder.SampleMethod.Name.ToLower().Contains("self") || testOrder.SampleMethod.Name.ToLower().Contains("tự"))
                {

                    sample.CollectedBy = testOrder.UserId;
                }
                else
                {
                    sample.CollectedBy = claim.Id;
                }

               
                sample.SampleMethodId = testOrder.SampleMethodId; // Set SampleMethodId from TestOrder
                await _unitOfWork.Samples.AddAsync(sample);
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