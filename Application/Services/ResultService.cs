using Application.Repository;
using Application.Response;
using Application.Request;
using AutoMapper;
using Domain.Entity;
using Application.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;

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
                    q => q.Include(r => r.Sample)
                          .ThenInclude(s => s.TestOrder)
                              .ThenInclude(t => t.Service)
                          .Include(r => r.Sample)
                              .ThenInclude(s => s.TestOrder)
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

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var result = await _unitOfWork.Results.GetAsync(r => r.Id == id);
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
                var result = await _unitOfWork.Samples.GetAsync(r => r.Id == request.SampleId);
                if (result == null) return response.SetNotFound("Sample not found");
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
                // Check duplicate nếu cần
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
                    null,
                    q => q.Include(r => r.Sample)
                          .ThenInclude(s => s.TestOrder)
                              .ThenInclude(t => t.Service)
                          .Include(r => r.Sample)
                              .ThenInclude(s => s.TestOrder)
                              .ThenInclude(t => t.SampleMethod)
                );
                var filtered = results.Where(r => r.Sample != null && r.Sample.TestOrder != null && r.Sample.TestOrder.UserId == claim.Id);
                var mapped = _mapper.Map<IEnumerable<ResultResponse>>(filtered);
                return response.SetOk(mapped);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
} 