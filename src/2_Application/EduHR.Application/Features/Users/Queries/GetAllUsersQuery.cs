using EduHR.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EduHR.Application.Features.Users.Queries;

/// <summary>
/// Bir kiracıya ait tüm kullanıcıları getirmek için sorguyu temsil eder.
/// </summary>
public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
    // Bu sorgu, o anki kullanıcının TenantId'sini ICurrentUserService üzerinden alacağı için
    // dışarıdan bir parametre almasına gerek yoktur.
}