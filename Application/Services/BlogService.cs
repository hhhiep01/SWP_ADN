using Application.Interface;
using Application.Request.Blog;
using Application.Response;
using Application.Response.Blog;
using AutoMapper;
using Domain.Entity;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var blogs = await _unitOfWork.Blogs.GetAllAsync(null, q => q.Include(s => s.UserAccount));
                var dtos = _mapper.Map<IEnumerable<BlogResponse>>(blogs);
                return new ApiResponse().SetOk(dtos);
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
                var blog = await _unitOfWork.Blogs.GetAsync(
                    x => x.Id == id,
                    q => q.Include(s => s.UserAccount)
                );

                if (blog == null)
                {
                    return response.SetNotFound($"Blog with ID {id} not found");
                }

                var result = _mapper.Map<BlogResponse>(blog);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(CreateBlogRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var claim = _claimService.GetUserClaim();
                var blog = _mapper.Map<Blog>(request);
                blog.UserAccountId = claim.Id;
                blog.CreatedDate = DateTime.UtcNow;

                await _unitOfWork.Blogs.AddAsync(blog);
                await _unitOfWork.SaveChangeAsync();

                var result = _mapper.Map<BlogResponse>(blog);
                return response.SetOk("Create Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(UpdateBlogRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existingBlog = await _unitOfWork.Blogs.GetAsync(
                    x => x.Id == request.Id,
                    include: query => query.Include(x => x.UserAccount)
                );
                if (existingBlog == null)
                {
                    return response.SetNotFound($"Blog with ID {request.Id} not found");
                }

                var claim = _claimService.GetUserClaim();
                if (existingBlog.UserAccountId != claim.Id)
                {
                    return response.SetBadRequest("You don't have permission to update this blog");
                }

                _mapper.Map(request, existingBlog);
                existingBlog.ModifiedDate = DateTime.UtcNow;

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
                var blog = await _unitOfWork.Blogs.GetAsync(
                    x => x.Id == id,
                    include: query => query.Include(x => x.UserAccount)
                );
                if (blog == null)
                {
                    return response.SetNotFound($"Blog with ID {id} not found");
                }

                var claim = _claimService.GetUserClaim();
                if (blog.UserAccountId != claim.Id)
                {
                    return response.SetBadRequest("You don't have permission to delete this blog");
                }

                await _unitOfWork.Blogs.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return response.SetOk("Blog deleted successfully");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
} 