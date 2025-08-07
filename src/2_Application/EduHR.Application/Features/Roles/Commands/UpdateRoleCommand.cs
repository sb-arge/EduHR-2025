using EduHR.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EduHR.Application.Features.Roles.Commands;

/// <summary>
/// Mevcut bir kiracıya özel rolü güncellemek için komutu temsil eder.
/// </summary>
public class UpdateRoleCommand : IRequest<RoleDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
}