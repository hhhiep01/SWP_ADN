using Application.Repository;
using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Application.Request;
using Application.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using Application.Interface;
using System.Security.Claims;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Application.Services
{
    public class CommentService: ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService= claimService;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var comments = await _unitOfWork.Comments.GetAllAsync(null, q => q.Include(c => c.UserAccount));
                var result = _mapper.Map<IEnumerable<CommentResponse>>(comments);
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
                var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == id, q => q.Include(c => c.UserAccount));
                if (comment == null) return response.SetNotFound("Comment not found");
                var result = _mapper.Map<CommentResponse>(comment);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> CreateAsync(CommentRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var blog = await _unitOfWork.Blogs.GetAsync(c => c.Id == request.BlogId);
                if (blog == null) return response.SetNotFound("Blog not found");
                var claim = _claimService.GetUserClaim();
                var comment = _mapper.Map<Comment>(request);
                comment.UserAccountId = claim.Id;
                comment.CreatedDate = DateTime.UtcNow;
                await _unitOfWork.Comments.AddAsync(comment);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk("Create Success");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> UpdateAsync(UpdateCommentRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var claim = _claimService.GetUserClaim();
                var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == request.Id);
                if (comment == null) return response.SetNotFound("Comment not found");
                if (comment.UserAccountId != claim.Id) return response.SetBadRequest("You don't have permission to update this comment");
                _mapper.Map(request, comment);
                comment.ModifiedDate = DateTime.UtcNow;
                await _unitOfWork.SaveChangeAsync();
                var result = _mapper.Map<CommentResponse>(comment);
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
                var claim = _claimService.GetUserClaim();
                var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == id);
                if (comment == null) return response.SetNotFound("Comment not found");
                if (comment.UserAccountId != claim.Id) return response.SetBadRequest("You don't have permission to delete this comment");
                await _unitOfWork.Comments.RemoveByIdAsync(comment.Id);
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk();
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetByBlogIdAsync(int blogId)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var blog = await _unitOfWork.Blogs.GetAsync(b => b.Id == blogId);
                if (blog == null) return response.SetNotFound("Blog not found");
                var comments = await _unitOfWork.Comments.GetAllAsync(c => c.BlogId == blogId, q => q.Include(c => c.UserAccount));
                var result = _mapper.Map<IEnumerable<CommentResponse>>(comments);
                return response.SetOk(result);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest($"Error: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
} 