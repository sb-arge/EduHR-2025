using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Roles.Commands;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity; // RoleManager için
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Roles.Handlers;

/// <summary>
/// Handles the CreateRoleCommand.
/// </summary>
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(RoleManager<Role> roleManager, ICurrentUserService currentUserService, IMapper mapper)
    {
        _roleManager = roleManager;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        // Aynı isimde, aynı kiracıya ait başka bir rol var mı kontrol et.
        var roleExists = await _roleManager.FindByNameAsync(request.Name) is Role existingRole && existingRole.TenantId == tenantId;
        if (roleExists)
        {
            throw new Exception($"'{request.Name}' adında bir rol bu kiracı için zaten mevcut."); // Daha spesifik bir Exception kullanılabilir.
        }

        // Yeni rol varlığını oluştur.
        var newRole = new Role
        {
            Name = request.Name,
            Description = request.Description,
            TenantId = tenantId
        };
        
        // Identity kullanarak rolü veritabanına kaydet.
        var result = await _roleManager.CreateAsync(newRole);
        if (!result.Succeeded)
        {
            throw new Exception($"Rol oluşturulamadı: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // İstenen yetkileri (permissions) Claim olarak role ekle.
        foreach (var permission in request.Permissions)
        {
            await _roleManager.AddClaimAsync(newRole, new Claim("Permission", permission));
        }

        return _mapper.Map<RoleDto>(newRole);
    }
}