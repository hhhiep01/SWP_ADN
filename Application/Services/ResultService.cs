using Application.Repository;
using Application.Response;
using Application.Request;
using AutoMapper;
using Domain.Entity;
using Application.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Application.Response.Sample;
using Application.Response.TestOrder;

namespace Application.Services
{
    public class ResultService : IResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public ResultService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
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
                var results = await _unitOfWork.Results.GetAllAsync(
                    null,
                    q => q.Include(r => r.TestOrder)
                          .ThenInclude(t => t.Service)
                          .Include(r => r.TestOrder)
                          .ThenInclude(t => t.SampleMethod)
                );
                results = results.OrderByDescending(r => r.CreatedDate).ToList();
                var mapped = _mapper.Map<IEnumerable<ResultResponse>>(results);
                return response.SetOk(mapped);
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
                var result = await _unitOfWork.Results.GetAsync(r => r.Id == id, q => q.Include(r => r.TestOrder));
                if (result == null) return response.SetNotFound("Result not found");
                var mapped = _mapper.Map<ResultResponse>(result);
                return response.SetOk(mapped);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(ResultRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var testOrder = await _unitOfWork.TestOrders.GetAsync(t => t.Id == request.TestOrderId);
                if (testOrder == null) return response.SetNotFound("TestOrder not found");
                var entity = _mapper.Map<Result>(request);
                await _unitOfWork.Results.AddAsync(entity);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk("Create Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(int id, ResultRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existing = await _unitOfWork.Results.GetAsync(r => r.Id == id);
                if (existing == null) return response.SetNotFound("Result not found");
                _mapper.Map(request, existing);
                await _unitOfWork.SaveChangeAsync();
                var mapped = _mapper.Map<ResultResponse>(existing);
                return response.SetOk(mapped);
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
                var existing = await _unitOfWork.Results.GetAsync(r => r.Id == id);
                if (existing == null) return response.SetNotFound("Result not found");
                await _unitOfWork.Results.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk();
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetByCurrentUserAsync()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var claim = _claimService.GetUserClaim();
                var results = await _unitOfWork.Results.GetAllAsync(
                    r => r.TestOrder.UserId == claim.Id,
                    q => q.Include(r => r.TestOrder)
                          .ThenInclude(t => t.Service)
                          .Include(r => r.TestOrder)
                          .ThenInclude(t => t.SampleMethod)
                );
                var mapped = _mapper.Map<IEnumerable<ResultResponse>>(results);
                return response.SetOk(mapped);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetFullResultByTestOrderIdAsync(int testOrderId)
        {
            var testOrder = await _unitOfWork.TestOrders.GetAsync(
                t => t.Id == testOrderId,
                q => q.Include(t => t.Samples)
            );
            if (testOrder == null)
                return new ApiResponse().SetNotFound("TestOrder not found");

            // Result gắn với TestOrder, không còn gắn với Sample
            var result = await _unitOfWork.Results.GetAsync(r => r.TestOrderId == testOrderId);

            var sampleResults = new List<SampleWithResultResponse>();
            foreach (var sample in testOrder.Samples)
            {
                var locusResults = await _unitOfWork.Locus.GetAllAsync(l => l.SampleId == sample.Id);
                var sampleWithResult = _mapper.Map<SampleWithResultResponse>(sample);
                sampleWithResult.Result = _mapper.Map<ResultResponse>(result); // result chung cho TestOrder
                sampleWithResult.LocusResults = _mapper.Map<List<LocusResultResponse>>(locusResults);
                sampleResults.Add(sampleWithResult);
            }

            var orderInfo = _mapper.Map<TestOrderWithResultResponse>(testOrder);
            orderInfo.Samples = sampleResults;

            var response = new TestOrderFullResultResponse
            {
                TestOrderId = testOrderId,
                OrderInfo = orderInfo
            };

            return new ApiResponse().SetOk(response);
        }
    }
} 