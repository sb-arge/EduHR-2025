using EduHR.Application.Features.Roles.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Roles;

/// <summary>
/// Validates the CreateRoleCommand.
/// </summary>
public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Role Name"])
            .MaximumLength(100).WithMessage(localizer["FieldCannotExceedLength", "Role Name", 100]);

        RuleFor(p => p.Permissions)
            .NotNull().WithMessage(localizer["FieldCannotBeNull", "Permissions List"]);
    }
}