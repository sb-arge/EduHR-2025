using EduHR.Application.Features.Users.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Users;

/// <summary>
/// Validates the UpdateUserRolesCommand.
/// </summary>
public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
{
    public UpdateUserRolesCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.UserId)
            .GreaterThan(0).WithMessage(localizer["FieldMustBeValid", "User ID"]);

        RuleFor(p => p.Roles)
            .NotNull().WithMessage(localizer["FieldCannotBeNull", "Roles List"]);
    }
}