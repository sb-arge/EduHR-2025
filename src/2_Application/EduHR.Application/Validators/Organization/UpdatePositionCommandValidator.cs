using EduHR.Application.Features.Organization.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Organization;

/// <summary>
/// Validates the UpdatePositionCommand.
/// </summary>
public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
{
    public UpdatePositionCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "ID"]);

        RuleFor(p => p.Title)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Position Title"])
            .MaximumLength(150).WithMessage(localizer["FieldCannotExceedLength", "Position Title", 150]);

        RuleFor(p => p.DepartmentId)
            .GreaterThan(0).WithMessage(localizer["FieldMustBeValid", "Department"]);
    }
}