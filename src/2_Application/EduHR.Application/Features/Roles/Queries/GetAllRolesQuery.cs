using EduHR.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EduHR.Application.Features.Roles.Queries;

/// <summary>
/// Bir kiracıda mevcut olan tüm rolleri (sistem ve kiracıya özel) getirmek için sorguyu temsil eder.
/// </summary>
public class GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
{
    // Bu sorgu, o anki kullanıcının TenantId'sini ICurrentUserService üzerinden alacaktır.
}