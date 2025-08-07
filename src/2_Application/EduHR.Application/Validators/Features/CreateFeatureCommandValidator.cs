using EduHR.Application.Features.Features.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Features;

/// <summary>
/// Validates the CreateFeatureCommand.
/// </summary>
public class CreateFeatureCommandValidator : AbstractValidator<CreateFeatureCommand>
{
    public CreateFeatureCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.FeatureCode)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Feature Code"])
            .MaximumLength(50).WithMessage(localizer["FieldCannotExceedLength", "Feature Code", 50]);

        RuleFor(p => p.Translations)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Translations"]);
            
        RuleForEach(p => p.Translations).ChildRules(translation => 
        {
            translation.RuleFor(t => t.LanguageCode).NotEmpty().MaximumLength(5);
            translation.RuleFor(t => t.Name).NotEmpty().MaximumLength(100);
        });
    }
}