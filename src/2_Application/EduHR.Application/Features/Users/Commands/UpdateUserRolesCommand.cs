using MediatR;
using System.Collections.Generic;

namespace EduHR.Application.Features.Users.Commands;

/// <summary>
/// Bir kullanıcının rollerini güncellemek için komutu temsil eder.
/// </summary>
public class UpdateUserRolesCommand : IRequest
{
    /// <summary>
    /// Rolleri güncellenecek olan kullanıcının kimliği.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Kullanıcıya atanacak olan yeni rollerin ad listesi.
    /// Bu liste, kullanıcının mevcut tüm rollerinin üzerine yazılacaktır.
    /// </summary>
    public IList<string> Roles { get; set; } = new List<string>();
}