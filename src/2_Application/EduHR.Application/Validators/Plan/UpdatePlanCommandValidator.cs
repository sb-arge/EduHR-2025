using EduHR.Application.Features.Plans.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Plans;

public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
{
    public UpdatePlanCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.Id)
            .NotEmpty();
            
        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0).WithMessage(localizer["FieldMustBePositiveOrZero", "Price"]);
            
        RuleFor(p => p.UserLimit)
            .GreaterThan(0).WithMessage(localizer["FieldMustBeGreaterThanZero", "User Limit"]);
    }
}