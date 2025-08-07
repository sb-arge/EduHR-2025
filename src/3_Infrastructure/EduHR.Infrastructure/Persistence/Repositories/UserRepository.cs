using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the repository for the User entity, using UserManager.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString());
    }
    
    public async Task<IEnumerable<User>> GetAllByTenantIdAsync(int tenantId)
    {
        // UserManager.Users, tüm kullanıcıları IQueryable olarak sunar.
        // Bu sayede, kiracıya özel filtreleme yapabiliriz.
        return await _userManager.Users
            .Where(u => u.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<User?> GetByIdWithRolesAsync(int userId)
    {
        // Bu metot, Identity'nin kendi mekanizmalarını kullandığı için
        // rolleri doğrudan user nesnesine "Include" edemeyiz.
        // Bu mantık genellikle Application katmanında, user'ı çektikten sonra
        // _userManager.GetRolesAsync(user) çağrılarak çözülür.
        // Ancak repository'de de bu şekilde birleştirilebilir.
        var user = await _userManager.FindByIdAsync(userId.ToString());
        // Roller, Application katmanında ayrıca yüklenecektir.
        return user;
    }
}