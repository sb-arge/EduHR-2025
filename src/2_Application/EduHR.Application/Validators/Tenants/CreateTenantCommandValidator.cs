using EduHR.Application.Features.Tenants.Commands;
using EduHR.Infrastructure.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EduHR.Application.Validators.Tenants;

/// <summary>
/// Validates the CreateTenantCommand.
/// </summary>
public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantCommandValidator(IStringLocalizer<SharedResources> localizer)
    {
        RuleFor(p => p.CompanyName)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Company Name"])
            .MaximumLength(200).WithMessage(localizer["FieldCannotExceedLength", "Company Name", 200]);
            
        RuleFor(p => p.Subdomain)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Subdomain"])
            .MaximumLength(50).WithMessage(localizer["FieldCannotExceedLength", "Subdomain", 50])
            .Matches("^[a-z0-9-]+$").WithMessage(localizer["FieldInvalidFormat", "Subdomain"]); // Sadece küçük harf, rakam ve tire

        RuleFor(p => p.AdminFirstName)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "First Name"])
            .MaximumLength(100);

        RuleFor(p => p.AdminLastName)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Last Name"])
            .MaximumLength(100);

        RuleFor(p => p.AdminEmail)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Email"])
            .EmailAddress().WithMessage(localizer["FieldInvalidEmailFormat"]);

        RuleFor(p => p.AdminPassword)
            .NotEmpty().WithMessage(localizer["FieldCannotBeEmpty", "Password"])
            .MinimumLength(6).WithMessage(localizer["FieldMustBeAtLeast", "Password", 6]);

        RuleFor(p => p.PlanId)
            .GreaterThan(0).WithMessage(localizer["FieldMustBeGreaterThanZero", "Plan"]);
    }
}