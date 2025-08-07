using EduHR.Application.Interfaces;
using EduHR.Common.Wrappers; 
using EduHR.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Identity;

/// <summary>
/// Implements the IIdentityService interface using ASP.NET Core Identity.
/// </summary>
public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;

    public IdentityService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(ApiResponse Result, int UserId)> CreateUserAsync(int tenantId, string firstName, string lastName, string email, string password, List<string> roles)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            // --- 2. HATA ÇÖZÜMÜ: Yeni ApiResponse kullanımı ---
            return (ApiResponse.FailResponse($"'{email}' adresine sahip bir kullanıcı zaten mevcut."), 0);
        }

        var newUser = new User
        {
            TenantId = tenantId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = email,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            // --- 2. HATA ÇÖZÜMÜ: Yeni ApiResponse kullanımı ---
            return (ApiResponse.FailResponse(errors), 0);
        }

        // Kullanıcıyı belirtilen rollere ata
        await _userManager.AddToRolesAsync(newUser, roles);

        // --- 2. HATA ÇÖZÜMÜ: Yeni ApiResponse kullanımı ---
        return (ApiResponse.SuccessResponse("Kullanıcı başarıyla oluşturuldu."), newUser.Id);
    }
}