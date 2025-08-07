using AutoMapper;
using EduHR.Application.Features.Departments.Queries;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Departments.Handlers;

/// <summary>
/// Handles the GetAllDepartmentsQuery.
/// </summary>
public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAllDepartmentsQueryHandler(
        IDepartmentRepository departmentRepository, 
        IMapper mapper, 
        ICurrentUserService currentUserService)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        // O anki kullanıcının kiracı kimliğini al (Çoklu-Kiracılık Güvenliği)
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        // Repository'den sadece o kiracıya ait departmanları, hiyerarşik olarak çek
        var departments = await _departmentRepository.GetAllByTenantIdAsHierarchyAsync(tenantId);
        
        // Sonucu DTO listesine dönüştür ve geri dön
        return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
    }
}