using CoreLayer.Entities.Abstract;

namespace EntityLayer.DTOs.Contact;

public class ContactGetDto:IDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanySite { get; set; }
    public string Message { get; set; }
}
