using EduHR.Application.Features.Organization.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Organization;

/// <summary>
/// Validates the CreatePositionCommand.
/// </summary>
public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    public CreatePositionCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Position Title"])
            .MaximumLength(150).WithMessage(localizer["FieldCannotExceedLength", "Position Title", 150]);

        RuleFor(p => p.DepartmentId)
            .GreaterThan(0).WithMessage(localizer["FieldMustBeValid", "Department"]);
    }
}