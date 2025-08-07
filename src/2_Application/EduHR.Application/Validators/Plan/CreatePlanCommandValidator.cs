using EduHR.Application.Features.Plans.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Plans;

/// <summary>
/// Validates the CreatePlanCommand.
/// </summary>
public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.PlanCode)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Plan Code"])
            .MaximumLength(50).WithMessage(localizer["FieldCannotExceedLength", "Plan Code", 50]);

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0).WithMessage(localizer["FieldMustBePositiveOrZero", "Price"]);
            
        RuleFor(p => p.UserLimit)
            .GreaterThan(0).WithMessage(localizer["FieldMustBeGreaterThanZero", "User Limit"]);
            
        RuleFor(p => p.Translations)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Translations"]);
            
        // Her bir çevirinin içini de kontrol edebiliriz.
        RuleForEach(p => p.Translations).ChildRules(translation => 
        {
            translation.RuleFor(t => t.LanguageCode).NotEmpty().MaximumLength(5);
            translation.RuleFor(t => t.Name).NotEmpty().MaximumLength(100);
        });
    }
}