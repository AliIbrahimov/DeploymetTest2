using CoreLayer.Utilities.Results;
using EntityLayer.DTOs.Blog;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.Abstract;

public interface IBlogService
{
    Task<IDataResult<List<BlogGetDTO>>> GetAllBlogs();
    Task<IDataResult<List<BlogGetDTO>>> GetAllPaginateAsync(int page, int size);
    Task<IDataResult<BlogGetDTO>> GetByIdAsync(int id);
    Task<IDataResult<BlogGetDTO>> GetByNameAsync(string name);
    Task<CoreLayer.Utilities.Results.IResult> CreateBlog(BlogPostDTO blogPostDto,IFormFile file); 
    Task<CoreLayer.Utilities.Results.IResult> UpdateBlog(int id,BlogPostDTO blogPostDto);
    Task<CoreLayer.Utilities.Results.IResult> HardDeleteByIdAsync(int id);
    Task<CoreLayer.Utilities.Results.IResult> SoftDeleteByIdAsync(int id);
    Task<CoreLayer.Utilities.Results.IResult> RecoverById(int id);
}
