using AutoMapper;
using EduHR.Application.Features.Roles.Queries;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Roles.Handlers;

/// <summary>
/// Handles the GetAllRolesQuery.
/// </summary>
public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetAllRolesQueryHandler(RoleManager<Role> roleManager, ICurrentUserService currentUserService, IMapper mapper)
    {
        _roleManager = roleManager;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        // TenantId'si null (Sistem Rolleri) VEYA mevcut kiracının Id'sine eşit olan rolleri getir.
        var roles = await _roleManager.Roles
            .Where(r => r.TenantId == null || r.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}