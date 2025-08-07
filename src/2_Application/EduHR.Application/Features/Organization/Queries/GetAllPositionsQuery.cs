using EduHR.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EduHR.Application.Features.Organization.Queries;

/// <summary>
/// Bir kiracıya ait tüm pozisyonları getirmek için sorguyu temsil eder.
/// </summary>
public class GetAllPositionsQuery : IRequest<IEnumerable<PositionDto>>
{
    /// <summary>
    /// Pozisyonları belirli bir departmana göre filtrelemek için (isteğe bağlı).
    /// </summary>
    public int? DepartmentId { get; set; }
}