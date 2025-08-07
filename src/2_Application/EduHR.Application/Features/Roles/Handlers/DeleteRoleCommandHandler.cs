using EduHR.Application.Exceptions;
using EduHR.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduHR.Application.Features.Roles.Commands;

namespace EduHR.Application.Features.Roles.Handlers;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public DeleteRoleCommandHandler(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var roleToDelete = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (roleToDelete is null)
        {
            throw new NotFoundException(nameof(Role), request.Id);
        }

        // --- İŞ KURALI KONTROLÜ ---
        // Bu role atanmış herhangi bir kullanıcı olup olmadığını kontrol et.
        var usersInRole = await _userManager.GetUsersInRoleAsync(roleToDelete.Name!);
        if (usersInRole.Any())
        {
            throw new EntityCannotBeDeletedException("Bu rol, aktif kullanıcılar tarafından kullanıldığı için silinemez.");
        }

        await _roleManager.DeleteAsync(roleToDelete);
    }
}