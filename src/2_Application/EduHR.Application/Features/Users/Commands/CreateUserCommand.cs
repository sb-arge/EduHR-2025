using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Users.Commands;

/// <summary>
/// Yeni bir kiracıya özel kullanıcı oluşturmak için komutu temsil eder.
/// </summary>
public class CreateUserCommand : IRequest<UserDto>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcıya oluşturulurken atanacak olan rollerin ad listesi.
    /// </summary>
    public List<string> Roles { get; set; } = new List<string>();
}