using EduHR.Application.Features.Plans.Commands;
using EduHR.Domain.Enums;
using MediatR;

namespace EduHR.Application.Features.Plans.Commands;

public class CreatePlanCommand : IRequest<CreatePlanCommandResponse>
{
    public string PlanCode { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public BillingCycle BillingCycle { get; set; }
    public int UserLimit { get; set; }
    public ICollection<PlanTranslationDto> Translations { get; set; } = new List<PlanTranslationDto>();
}

public class PlanTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreatePlanCommandResponse
{
    public int Id { get; set; }
    public string PlanCode { get; set; } = string.Empty;
}