using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Departments.Queries;

/// <summary>
/// Bir kiracıya ait tüm departmanları getirmek için sorguyu temsil eder.
/// </summary>
public class GetAllDepartmentsQuery : IRequest<IEnumerable<DepartmentDto>>
{
    // Bu sorgu, o anki kullanıcının TenantId'sini ICurrentUserService üzerinden alacağı için
    // dışarıdan bir parametre almasına gerek yoktur.
}