using EntityLayer.DTOs.Statistic;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class StatisticValidation: AbstractValidator<PostStatisticDto>
{
	public StatisticValidation()
	{
		RuleFor(s => s.WinAwards).GreaterThan(0);
		RuleFor(s => s.ProjectsDone).GreaterThan(0);
		RuleFor(s => s.Customer).GreaterThan(0);
	}
}
