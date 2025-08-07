using EduHR.Common.DTOs; // <-- DÜZELTİLDİ: Artık Common projesinden geliyor.
using MediatR;

namespace EduHR.Application.Features.Plans.Queries;

/// <summary>
/// Represents the query to get all plans with pagination.
/// </summary>
public class GetAllPlansQuery : IRequest<PagedResultDto<PlanDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}