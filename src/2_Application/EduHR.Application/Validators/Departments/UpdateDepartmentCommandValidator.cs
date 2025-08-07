using EduHR.Application.Features.Departments.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Departments;

/// <summary>
/// Validates the UpdateDepartmentCommand.
/// </summary>
public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    // Constructor'ın içi aynı kalır.
    public UpdateDepartmentCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "ID"]);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Department Name"])
            .MaximumLength(100).WithMessage(localizer["FieldCannotExceedLength", "Department Name", 100]);
    }
}