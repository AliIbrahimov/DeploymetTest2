using CoreLayer.Entities.Concrete;

namespace EntityLayer.Models;

public class Information : Entity
{
    public Information()
    {
        SocialMediaAccounts = new List<SocialMediaAccount>();
    }
    public string AgencyMail { get; set; }
    public string AgencyLocation { get; set; }
    public string AgencyPhone { get; set; }
    public ICollection<SocialMediaAccount>? SocialMediaAccounts { get; set; }
}
