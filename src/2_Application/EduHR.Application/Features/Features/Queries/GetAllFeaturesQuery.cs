using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Features.Queries;

/// <summary>
/// Represents the query to get all features with pagination.
/// </summary>
public class GetAllFeaturesQuery : IRequest<PagedResultDto<FeatureDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}