using Application.Interface;
using Application.Repository;
using Application.Request;
using Application.Response;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LocusResultService : ILocusResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LocusResultService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var response = new ApiResponse();
            try
            {
                var entities = await _unitOfWork.Locus
                    .GetAllAsync(null, q => q.Include(l => l.Sample));

                var dtoList = _mapper.Map<IEnumerable<LocusResultResponse>>(entities);
                return response.SetOk(dtoList);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var response = new ApiResponse();
            try
            {
                var entity = await _unitOfWork.Locus.GetAsync(l => l.Id == id);
                if (entity == null)
                    return response.SetNotFound("LocusResult not found");

                var dto = _mapper.Map<LocusResultResponse>(entity);
                return response.SetOk(dto);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(LocusResultRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var sample = await _unitOfWork.Samples.GetAsync(s => s.Id == request.SampleId);
                if (sample == null)
                    return response.SetNotFound("Sample not found");

                var entities = _mapper.Map<List<LocusResult>>(request);

                await _unitOfWork.Locus.AddRangeAsync(entities);
                await _unitOfWork.SaveChangeAsync();

                return response.SetOk("Create Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(int sampleId, LocusResultRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var sample = await _unitOfWork.Samples.GetAsync(s => s.Id == sampleId);
                if (sample == null) return response.SetNotFound("Sample not found");
                // Remove all old LocusResults for this sample
                var oldLocusResults = await _unitOfWork.Locus.GetAllAsync(l => l.SampleId == sampleId);
                foreach (var old in oldLocusResults)
                {
                    await _unitOfWork.Locus.RemoveByIdAsync(old.Id);
                }
                await _unitOfWork.SaveChangeAsync();
                // Add new ones
                request.SampleId = sampleId;
                var newEntities = _mapper.Map<List<LocusResult>>(request);

                await _unitOfWork.Locus.AddRangeAsync(newEntities);
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
                var existing = await _unitOfWork.Locus.GetAsync(l => l.Id == id);
                if (existing == null) return response.SetNotFound("LocusResult not found");
                await _unitOfWork.Locus.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk();
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetBySampleIdAsync(int sampleId)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var sample = await _unitOfWork.Samples.GetAsync(s => s.Id == sampleId);
                if (sample == null) return response.SetNotFound("Sample not found");
                var locusResults = await _unitOfWork.Locus.GetAllAsync(l => l.SampleId == sampleId);
                var mapped = _mapper.Map<IEnumerable<LocusResultResponse>>(locusResults);
                return response.SetOk(mapped);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
        public async Task<ApiResponse> GetBySampleCodeAsync(string sampleCode)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var sample = await _unitOfWork.Samples.GetAsync(s => s.SampleCode == sampleCode);
                if (sample == null) return response.SetNotFound("Sample not found");
                var locusResults = await _unitOfWork.Locus.GetAllAsync(l => l.SampleId == sample.Id);
                var mapped = _mapper.Map<IEnumerable<LocusResultResponse>>(locusResults);
                return response.SetOk(mapped);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
} 