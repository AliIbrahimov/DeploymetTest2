using BusinessLayer.Services.Abstract;
using EntityLayer.DTOs.Blog;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.DevTools.V113.Database;

namespace AdaliaMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase

    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("getall")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _blogService.GetAllBlogs();
            if (result.Success) return Ok(result);
            return BadRequest();
        }
        [HttpGet("getallPaginated")]

        public async Task<IActionResult> GetAllPaginated(int page,int size)
        {
            var result = await _blogService.GetAllPaginateAsync(page,size);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getbyid")]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await _blogService.GetByIdAsync(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getbyname")]

        public async Task<IActionResult> GetByName(string title)
        {
            var result = await _blogService.GetByNameAsync(title);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("add")]

        public async Task<IActionResult> Add([FromForm]BlogPostDTO blog)
        {
            var result = await _blogService.CreateBlog(blog, blog.FormFile) ;
            if (result.Success) return Ok(blog.ArticleContent);
            return BadRequest(result);
        }
        [HttpPut("update")]

        public async Task<IActionResult> Update(int id,[FromForm]BlogPostDTO blog)
        {
            var result = await _blogService.UpdateBlog(id,blog);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("delete")]

        public async Task<IActionResult> HardDelete(int id)
        {
            var result = await _blogService.HardDeleteByIdAsync(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("softDelete/id")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var result = await _blogService.SoftDeleteByIdAsync(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("recover/id")]
        public async Task<IActionResult> Recover(int id)
        {
            var result = await _blogService.RecoverById(id);
            if (result.Success) return Ok(result);
            return BadRequest();
        }

    }
}
