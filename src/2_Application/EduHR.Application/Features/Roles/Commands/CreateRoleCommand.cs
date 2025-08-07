using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Roles.Commands;

/// <summary>
/// Yeni bir kiracıya özel rol oluşturmak için komutu temsil eder.
/// </summary>
public class CreateRoleCommand : IRequest<RoleDto>
{
    /// <summary>
    /// Rolün adı (örn: "Bölüm Başkanı").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Rolün açıklaması.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Bu role atanacak olan yetkilerin (permission/claim) listesi.
    /// Örn: "personnel.create", "personnel.view.salary"
    /// </summary>
    public List<string> Permissions { get; set; } = new List<string>();
}