using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Organization.Commands;

/// <summary>
/// Mevcut bir pozisyonu güncellemek için komutu temsil eder.
/// </summary>
public class UpdatePositionCommand : IRequest<PositionDto>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
}