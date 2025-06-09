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

namespace Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var services = await _unitOfWork.Services.GetAllAsync(null, 
                    include: q => q.Include(s => s.ServiceSampleMethods)
                                 .ThenInclude(sms => sms.SampleMethod));
                var result = _mapper.Map<IEnumerable<ServiceResponse>>(services);
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
                var service = await _unitOfWork.Services.GetAsync(s => s.Id == id, q => q.Include(s => s.ServiceSampleMethods)
                                 .ThenInclude(sms => sms.SampleMethod));
                if (service == null) return response.SetNotFound("Service not found");
                var result = _mapper.Map<ServiceResponse>(service);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(ServiceRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existed = await _unitOfWork.Services.GetAsync(s => s.Name.ToLower() == request.Name.ToLower());
                if (existed != null)
                {
                    return response.SetBadRequest("Service name already exists.");
                }

                // Validate SampleMethodIds if provided
                if (request.SampleMethodIds != null && request.SampleMethodIds.Any())
                {
                    var sampleMethods = await _unitOfWork.SampleMethods.GetAllAsync(sm => request.SampleMethodIds.Contains(sm.Id));
                    if (sampleMethods.Count != request.SampleMethodIds.Count)
                    {
                        return response.SetBadRequest("One or more SampleMethod IDs are invalid.");
                    }
                }

                // Create service
                var service = _mapper.Map<Service>(request);
                await _unitOfWork.Services.AddAsync(service);
                await _unitOfWork.SaveChangeAsync();

                // Add SampleMethod relationships if provided
                if (request.SampleMethodIds != null && request.SampleMethodIds.Any())
                {
                    var sampleMethodServices = request.SampleMethodIds.Select(sampleMethodId => new ServiceSampleMethod
                    {
                        ServiceId = service.Id,
                        SampleMethodId = sampleMethodId
                    }).ToList();

                    await _unitOfWork.ServiceSampleMethods.AddRangeAsync(sampleMethodServices);
                    await _unitOfWork.SaveChangeAsync();
                }

                // Return created service with sample methods
                var result = await GetByIdAsync(service.Id);
                return result;
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(ServiceUpdateRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                // Check if service exists
                var existing = await _unitOfWork.Services.GetAsync(s => s.Id == request.Id, q => q.Include(s => s.ServiceSampleMethods));
                if (existing == null) return response.SetNotFound("Service not found");

                // Check duplicate name (except itself)
                var existed = await _unitOfWork.Services.GetAsync(s => s.Name.ToLower() == request.Name.ToLower() && s.Id != request.Id);
                if (existed != null)
                {
                    return response.SetBadRequest("Service name already exists.");
                }

                // Validate SampleMethodIds if provided
                if (request.SampleMethodIds != null && request.SampleMethodIds.Any())
                {
                    var sampleMethods = await _unitOfWork.SampleMethods.GetAllAsync(sm => request.SampleMethodIds.Contains(sm.Id));
                    if (sampleMethods.Count != request.SampleMethodIds.Count)
                    {
                        return response.SetBadRequest("One or more SampleMethod IDs are invalid.");
                    }
                }

                // Update service properties
                _mapper.Map(request, existing);
                await _unitOfWork.SaveChangeAsync();

                // Update SampleMethod relationships
                if (request.SampleMethodIds != null)
                {
                    // Remove existing relationships
                    var existingRelationships = await _unitOfWork.ServiceSampleMethods.GetAllAsync(sms => sms.ServiceId == request.Id);
                    foreach (var relationship in existingRelationships)
                    {
                        await _unitOfWork.ServiceSampleMethods.RemoveByIdAsync(relationship.Id);
                    }
                    await _unitOfWork.SaveChangeAsync();

                    // Add new relationships if provided
                    if (request.SampleMethodIds.Any())
                    {
                        var sampleMethodServices = request.SampleMethodIds.Select(sampleMethodId => new ServiceSampleMethod
                        {
                            ServiceId = request.Id,
                            SampleMethodId = sampleMethodId
                        }).ToList();

                        await _unitOfWork.ServiceSampleMethods.AddRangeAsync(sampleMethodServices);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }

                // Return updated service with sample methods
                var result = await GetByIdAsync(request.Id);
                return result;
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
                var existing = await _unitOfWork.Services.GetAsync(s => s.Id == id);
                if (existing == null) return response.SetNotFound("Service not found");

                // Remove SampleMethod relationships first (cascade delete will handle this)
                await _unitOfWork.Services.RemoveByIdAsync(id);
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