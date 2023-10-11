using CoreLayer.Entities.Abstract;

namespace EntityLayer.DTOs.Blog;

public class BlogGetDTO : IDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Title { get; set; }
    public string CoverImage { get; set; }
    public string? ArticleTitle { get; set; }
    public string? ArticleContent { get; set; }
}
