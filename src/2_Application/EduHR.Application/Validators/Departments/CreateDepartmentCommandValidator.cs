using EduHR.Application.Features.Departments.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Departments;

/// <summary>
/// Validates the CreateDepartmentCommand.
/// </summary>
public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Department Name"])
            .MaximumLength(100).WithMessage(localizer["FieldCannotExceedLength", "Department Name", 100]);
    }
}