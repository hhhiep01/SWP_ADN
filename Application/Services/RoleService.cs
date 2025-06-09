using Application.Interface;
using Application.Repository;
using Application.Response;
using Application.Response.Role;
using Application.Request.Role;
using AutoMapper;
using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var roles = await _unitOfWork.Roles.GetAllAsync(null);
                var result = _mapper.Map<IEnumerable<RoleResponse>>(roles);
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
                var role = await _unitOfWork.Roles.GetAsync(r => r.Id == id);
                if (role == null) return response.SetNotFound("Role not found");
                var result = _mapper.Map<RoleResponse>(role);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(RoleRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existed = await _unitOfWork.Roles.GetAsync(r => r.Name.ToLower() == request.Name.ToLower());
                if (existed != null)
                {
                    return response.SetBadRequest("Role name already exists.");
                }
                var role = _mapper.Map<Role>(request);
                await _unitOfWork.Roles.AddAsync(role);
                await _unitOfWork.SaveChangeAsync();
                var result = _mapper.Map<RoleResponse>(role);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(int id, RoleRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existing = await _unitOfWork.Roles.GetAsync(r => r.Id == id);
                if (existing == null) return response.SetNotFound("Role not found");
                // Check duplicate name (except itself)
                var existed = await _unitOfWork.Roles.GetAsync(r => r.Name.ToLower() == request.Name.ToLower() && r.Id != id);
                if (existed != null)
                {
                    return response.SetBadRequest(null, "Role name already exists.");
                }
                _mapper.Map(request, existing);
                await _unitOfWork.SaveChangeAsync();
                var result = _mapper.Map<RoleResponse>(existing);
                return response.SetOk(result);
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
                var existing = await _unitOfWork.Roles.GetAsync(r => r.Id == id);
                if (existing == null) return response.SetNotFound("Role not found");
                await _unitOfWork.Roles.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk();
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
} 