using Application.Interface;
using Application.Repository;
using Application.Response;
using Application.Response.Service;
using Application.Request.Service;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Response.SampleMethod;
using Application.Request.SampleMethod;

namespace Application.Services
{
    public class SampleMethodService : ISampleMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public SampleMethodService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
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
                var sampleMethods = await _unitOfWork.SampleMethods.GetAllAsync(null);
                var result = _mapper.Map<IEnumerable<SampleMethodResponse>>(sampleMethods);
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
                var sampleMethod = await _unitOfWork.SampleMethods.GetAsync(s => s.Id == id);
                if (sampleMethod == null) return response.SetNotFound("SampleMethod not found");
                var result = _mapper.Map<SampleMethodResponse>(sampleMethod);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(SampleMethodRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {   
                var existed = await _unitOfWork.SampleMethods.GetAsync(s => s.Name.ToLower() == request.Name.ToLower());
                if (existed != null)
                {
                    return response.SetBadRequest("SampleMethod name already exists.");
                }

                var sampleMethod = _mapper.Map<SampleMethod>(request);
                await _unitOfWork.SampleMethods.AddAsync(sampleMethod);
                await _unitOfWork.SaveChangeAsync();
                var result = _mapper.Map<SampleMethodResponse>(sampleMethod);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(SampleMethodUpdateRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existing = await _unitOfWork.SampleMethods.GetAsync(s => s.Id == request.Id);
                if (existing == null) return response.SetNotFound("SampleMethod not found");

                var existed = await _unitOfWork.SampleMethods.GetAsync(s => s.Name.ToLower() == request.Name.ToLower() && s.Id != request.Id);
                if (existed != null)
                {
                    return response.SetBadRequest("SampleMethod name already exists.");
                }

                _mapper.Map(request, existing);
                await _unitOfWork.SaveChangeAsync();
                var result = _mapper.Map<SampleMethodResponse>(existing);
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
                var existing = await _unitOfWork.SampleMethods.GetAsync(s => s.Id == id);
                if (existing == null) return response.SetNotFound("SampleMethod not found");

                await _unitOfWork.SampleMethods.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk("Delete Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
} 