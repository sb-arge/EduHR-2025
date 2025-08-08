using EduHR.Application.Features.Personnel.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Personnel;

/// <summary>
/// Validates the CreatePersonnelCommand.
/// </summary>
public class CreatePersonnelCommandValidator : AbstractValidator<CreatePersonnelCommand>
{
    public CreatePersonnelCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "First Name"])
            .MaximumLength(100);

        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Last Name"])
            .MaximumLength(100);
            
        RuleFor(p => p.Tckn)
            .MaximumLength(11).WithMessage(localizer["FieldCannotExceedLength", "TCKN", 11]);
            // Not: TCKN için daha gelişmiş bir algoritma kontrolü de eklenebilir.

        RuleFor(p => p.PositionId)
            .GreaterThan(0).WithMessage(localizer["FieldMustBeValid", "Position"]);

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Email"])
            .EmailAddress().WithMessage(localizer["FieldInvalidEmailFormat"]);

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Password"])
            .MinimumLength(8).WithMessage(localizer["FieldMustBeAtLeast", "Password", 8]);
            
        RuleFor(p => p.Roles)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Roles"]);
    }
}