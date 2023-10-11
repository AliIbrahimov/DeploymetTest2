using CoreLayer.Entities.Concrete;

namespace EntityLayer.Models;

public class SocialMediaAccount :Entity
{
    public string? Platform { get; set; }
    public string? Url { get; set; }
    public string? LogoUrl { get; set; }
    public int InfoId { get; set; }
    public Information? Info { get; set; }
}
