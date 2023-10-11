using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.DTOs.Blog;

public record BlogPostDTO
{
    public string? Title { get; set; }
    [NotMapped]
    public IFormFile? FormFile { get; set; }
    public string? ArticleTitle { get; set; }
    public string? ArticleContent { get; set; }
}
