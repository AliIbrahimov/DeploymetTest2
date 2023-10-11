using AutoMapper;
using BusinessLayer.Constants;
using BusinessLayer.Services.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using CoreLayer.Utilities.Extention;
using CoreLayer.Utilities.Results;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.DTOs.Blog;
using EntityLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace BusinessLayer.Services.Concrete;

public class BlogManager : IBlogService
{
    private readonly IBlogDAL _blogDAL;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly BlogValidator validator = new BlogValidator();


    public BlogManager(IBlogDAL blogDAL, IMapper mapper, IWebHostEnvironment env)
    {
        _blogDAL = blogDAL;
        _mapper = mapper;
        _env = env;
    }
    public async Task<IDataResult<List<BlogGetDTO>>> GetAllBlogs()
    {
        try
        {
            var blogs = _mapper.Map<List<BlogGetDTO>>(await _blogDAL.GetAllAsync(d => !d.isDeleted));
            if (blogs is null)
                return new ErrorDataResult<List<BlogGetDTO>>("Empty");
            return new SuccessDataResult<List<BlogGetDTO>>(blogs);
        }
        catch (Exception)
        {
            return new ErrorDataResult<List<BlogGetDTO>>("Somthing went wrong!");
        }

    }
    public async Task<IDataResult<List<BlogGetDTO>>> GetAllPaginateAsync(int page, int size)
    {
        try
        {
            var blogs = _mapper.Map<List<BlogGetDTO>>(await _blogDAL.GetAllPaginateAsync(page,size,b=>!b.isDeleted));
            if (blogs.Count is 0)
                return new ErrorDataResult<List<BlogGetDTO>>(Messages.NotFound);
            return new SuccessDataResult<List<BlogGetDTO>>(blogs);
        }
        catch (Exception)
        {
            return new ErrorDataResult<List<BlogGetDTO>>("Somthing went wrong!");
        }
    }

    public async Task<IDataResult<BlogGetDTO>> GetByIdAsync(int id)
    {
        try
        {
            var blog = _mapper.Map<BlogGetDTO>(await _blogDAL.GetAsync(p => p.Id == id && !p.isDeleted));
            if (blog is null)
                return new ErrorDataResult<BlogGetDTO>("Blog is null");
            return new SuccessDataResult<BlogGetDTO>(blog);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<BlogGetDTO>("No blog found on this id");
        }
    }


    public async Task<IDataResult<BlogGetDTO>> GetByNameAsync(string name)
    {
        try
        {
            if (!(await _blogDAL.IsExistAsync(b => b.Title == name)))
                return new ErrorDataResult<BlogGetDTO>("Blog not found.");
             
            var blog = _mapper.Map<BlogGetDTO>(await _blogDAL.GetAsync(p => p.Title == name && !p.isDeleted));
            
            return new SuccessDataResult<BlogGetDTO>(blog);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<BlogGetDTO>("No blog found on this title");
        }
    }

    public async Task<CoreLayer.Utilities.Results.IResult> CreateBlog(BlogPostDTO blogPostDto, IFormFile file)
    {
        try
        {
            //validation
            var validationResult = await validator.ValidateAsync(blogPostDto);
            if (!validationResult.IsValid)
            {
                string errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
                return new ErrorDataResult<Blog>(errorMessages);
            }
            if (file is null || !IsImageFile(file.FileName))
                return new ErrorDataResult<Blog>("Invalid file format. Please upload a valid image file.");
            Blog blog = _mapper.Map<Blog>(blogPostDto);
            blog.CoverImage = file.UploadFile(_env.WebRootPath, "uploads/BlogCover");
            await _blogDAL.CreateAsync(blog);

            return new SuccessResult(Messages.Added);
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Not added! Error: {ex.Message}");
        }
    }

    private bool IsImageFile(string fileName)
    {
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp", ".svg", ".ico" };
        string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        return Array.Exists(allowedExtensions, ext => ext == fileExtension);
    }

    public async Task<CoreLayer.Utilities.Results.IResult> UpdateBlog(int id, BlogPostDTO blogPostDto)
    {
        try
        {
            if (blogPostDto.FormFile is not null && !IsImageFile(blogPostDto.FormFile.FileName))
                return new ErrorDataResult<Blog>("Invalid file format. Please upload a valid image file.");

            if (!(await _blogDAL.IsExistAsync(b=>b.Id==id)))
                return new ErrorDataResult<Blog>("Blog not found.");
           
            Blog existingBlog = await _blogDAL.GetAsync(p => p.Id == id);



            if (blogPostDto.FormFile is not null)
            {

                FileExtension.DeleteFile(_env.WebRootPath, "uploads/BlogCover", existingBlog.CoverImage);
                existingBlog.CoverImage = blogPostDto.FormFile.UploadFile(_env.WebRootPath, "uploads/BlogCover");
            }

            if (!string.IsNullOrWhiteSpace(blogPostDto.Title))
                existingBlog.Title = blogPostDto.Title;

            if (!string.IsNullOrWhiteSpace(blogPostDto.ArticleTitle))
                existingBlog.ArticleTitle = blogPostDto.ArticleTitle;


            if (!string.IsNullOrWhiteSpace(blogPostDto.ArticleContent))
                existingBlog.ArticleContent = blogPostDto.ArticleContent;
            _blogDAL.Update(existingBlog);
            return new SuccessDataResult<BlogGetDTO>(_mapper.Map<BlogGetDTO>(existingBlog),Messages.Updated);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<Blog>($"Not Updated! Error: {ex.Message}");
        }
    }

    public async Task<CoreLayer.Utilities.Results.IResult> HardDeleteByIdAsync(int id)
    {
        if (!await _blogDAL.IsExistAsync(b=>b.Id==id))
            return new ErrorDataResult<Blog>("Blog not found.");

        Blog existingBlog = await _blogDAL.GetAsync(p => p.Id == id);
        FileExtension.DeleteFile(_env.WebRootPath, "uploads/BlogCover", existingBlog.CoverImage);
        _blogDAL.Delete(existingBlog);
        return new SuccessDataResult<Blog>(existingBlog, Messages.Deleted);
    }

    public async Task<CoreLayer.Utilities.Results.IResult> SoftDeleteByIdAsync(int id)
    {
        return await UpdateIsDeletedAsync(id, true, Messages.Deleted, Messages.NotFound);
    }

    public async Task<CoreLayer.Utilities.Results.IResult> RecoverById(int id)
    {
        return await UpdateIsDeletedAsync(id, false, Messages.Recovered, Messages.NotFound);
    }

    private async Task<CoreLayer.Utilities.Results.IResult> UpdateIsDeletedAsync(int id, bool isDeleted, string successMessage, string notFoundMessage)
    {
        try
        {
            if (!await _blogDAL.IsExistAsync(b=>b.Id==id))
                return new ErrorResult(notFoundMessage);

            Blog blog = await _blogDAL.GetAsync(b => b.Id == id && b.isDeleted == !isDeleted);
            blog.isDeleted = isDeleted;
            _blogDAL.Update(blog);

            return new SuccessResult(successMessage);
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }

    
}
