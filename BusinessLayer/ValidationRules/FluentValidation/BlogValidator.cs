using EntityLayer.DTOs.Blog;
using FluentValidation;
namespace BusinessLayer.ValidationRules.FluentValidation;

public class BlogValidator : AbstractValidator<BlogPostDTO>
{
	public BlogValidator()
	{
        RuleFor(b => b.Title).NotNull().MinimumLength(2);
        RuleFor(b=>b.ArticleTitle).NotEmpty().MinimumLength(2);
		RuleFor(b=>b.ArticleContent).NotEmpty().MinimumLength(2);
		RuleFor(b => b.FormFile).NotEmpty();
	}
}
