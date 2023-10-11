using CoreLayer.Entities.Concrete;

namespace EntityLayer.Models;

public class Blog : Entity
{
    public Blog()
    {
        CreatedDate = DateTime.Now;
        isDeleted = false;
    }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; } 
    public string CoverImage { get; set; }
    public string? ArticleTitle { get; set; }
    public string? ArticleContent { get; set; }
}
