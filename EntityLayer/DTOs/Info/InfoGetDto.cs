using EntityLayer.DTOs.SocialMedia;
using EntityLayer.Models;

namespace EntityLayer.DTOs.Info;

public class InfoGetDto
{
    public int Id { get; set; }
    public string AgencyMail { get; set; }
    public string AgencyLocation { get; set; }
    public string AgencyPhone { get; set; }
    public List<GetSocialMediaDto> SocialMediaAccounts { get; set; }
}
