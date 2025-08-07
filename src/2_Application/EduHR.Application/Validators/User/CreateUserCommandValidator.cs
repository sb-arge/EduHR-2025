using EduHR.Application.Features.Users.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Users;

/// <summary>
/// Validates the CreateUserCommand.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "First Name"])
            .MaximumLength(100);

        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Last Name"])
            .MaximumLength(100);
        
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Email"])
            .EmailAddress().WithMessage(localizer["FieldInvalidEmailFormat"]);

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Password"])
            .MinimumLength(6).WithMessage(localizer["FieldMustBeAtLeast", "Password", 6]);
            
        RuleFor(p => p.Roles)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Roles"]);
    }
}