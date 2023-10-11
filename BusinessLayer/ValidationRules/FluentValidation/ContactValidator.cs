using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Contact;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class ContactValidator : AbstractValidator<ContactPostDTO>
{
	public ContactValidator()
    {
        RuleFor(c => c.Email).MaximumLength(255).MinimumLength(3).NotNull().NotEmpty().EmailAddress();
        RuleFor(c => c.Message).MaximumLength(500).MinimumLength(2).NotEmpty();
        RuleFor(c=>c.Firstname).MaximumLength(255).MinimumLength(2);
        RuleFor(c=>c.Lastname).MaximumLength(255).MinimumLength(2);
        RuleFor(c=>c.CompanyName).MaximumLength(255).MinimumLength(2);
        RuleFor(c=>c.CompanySite).MaximumLength(255).MinimumLength(2);



    }
}
