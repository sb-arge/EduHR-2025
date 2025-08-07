using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Departments.Commands;

/// <summary>
/// Mevcut bir departmanı güncellemek için komutu temsil eder.
/// </summary>
public class UpdateDepartmentCommand : IRequest<DepartmentDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ParentDepartmentId { get; set; }
}