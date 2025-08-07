using EduHR.Application.Exceptions;
using EduHR.Application.Features.Users.Commands;
using EduHR.Application.Interfaces;
using EduHR.Domain.Entities;
using EduHR.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Users.Handlers;

/// <summary>
/// Handles the UpdateUserRolesCommand.
/// </summary>
public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public UpdateUserRolesCommandHandler(
        UserManager<User> userManager, 
        ICurrentUserService currentUserService,
        IMediator mediator)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public async Task Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        var userToUpdate = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (userToUpdate is null || userToUpdate.TenantId != tenantId)
        {
            // Kullanıcı bulunamadı veya bu kiracıya ait değilse hata fırlat.
            throw new NotFoundException(nameof(User), request.UserId);
        }

        // Kullanıcının mevcut rollerini al
        var currentRoles = await _userManager.GetRolesAsync(userToUpdate);

        // Önce mevcut tüm rolleri kaldır
        var removalResult = await _userManager.RemoveFromRolesAsync(userToUpdate, currentRoles);
        if (!removalResult.Succeeded)
        {
            throw new Exception($"Failed to remove existing roles for user {userToUpdate.Email}.");
        }

        // Yeni rolleri ekle
        var additionResult = await _userManager.AddToRolesAsync(userToUpdate, request.Roles);
        if (!additionResult.Succeeded)
        {
            throw new Exception($"Failed to add new roles for user {userToUpdate.Email}.");
        }
        
        // Rol değişikliği olayını yayınla
        await _mediator.Publish(new UserRolesChangedEvent(userToUpdate.Id, request.Roles), cancellationToken);
    }
}