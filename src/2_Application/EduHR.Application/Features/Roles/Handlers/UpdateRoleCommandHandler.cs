using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using EduHR.Application.Features.Roles.Commands;

namespace EduHR.Application.Features.Roles.Handlers;

/// <summary>
/// Handles the UpdateRoleCommand.
/// </summary>
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(RoleManager<Role> roleManager, ICurrentUserService currentUserService, IMapper mapper)
    {
        _roleManager = roleManager;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        var roleToUpdate = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (roleToUpdate is null || roleToUpdate.TenantId != tenantId)
        {
            throw new NotFoundException(nameof(Role), request.Id);
        }

        // Rolün adını ve açıklamasını güncelle
        roleToUpdate.Name = request.Name;
        roleToUpdate.Description = request.Description;
        await _roleManager.UpdateAsync(roleToUpdate);

        // Mevcut tüm "Permission" claim'lerini kaldır
        var currentClaims = await _roleManager.GetClaimsAsync(roleToUpdate);
        var permissionClaims = currentClaims.Where(c => c.Type == "Permission");
        foreach (var claim in permissionClaims)
        {
            await _roleManager.RemoveClaimAsync(roleToUpdate, claim);
        }

        // Yeni yetkileri (permissions) Claim olarak role ekle
        foreach (var permission in request.Permissions)
        {
            await _roleManager.AddClaimAsync(roleToUpdate, new Claim("Permission", permission));
        }

        return _mapper.Map<RoleDto>(roleToUpdate);
    }
}