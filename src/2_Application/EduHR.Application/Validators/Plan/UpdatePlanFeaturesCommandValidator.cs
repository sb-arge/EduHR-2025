using EduHR.Application.Features.Plans.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Plans;

/// <summary>
/// Validates the UpdatePlanFeaturesCommand.
/// </summary>
public class UpdatePlanFeaturesCommandValidator : AbstractValidator<UpdatePlanFeaturesCommand>
{
    public UpdatePlanFeaturesCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.PlanId)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Plan ID"]);

        RuleFor(p => p.FeatureIds)
            .NotNull().WithMessage(localizer["FieldCannotBeNull", "Feature List"]);
    }
}