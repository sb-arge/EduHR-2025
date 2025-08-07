using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Tenants.Queries;

/// <summary>
/// Tüm kiracıları sayfalama ile getirmek için sorguyu temsil eder.
/// </summary>
public class GetAllTenantsQuery : IRequest<PagedResultDto<TenantDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}