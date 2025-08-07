using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the repository for the Tenant entity.
/// </summary>
public class TenantRepository : GenericRepository<Tenant>, ITenantRepository
{
    public TenantRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a tenant by its unique subdomain.
    /// </summary>
    /// <param name="subdomain">The subdomain to search for.</param>
    /// <returns>The tenant if found; otherwise, null.</returns>
    public async Task<Tenant?> GetBySubdomainAsync(string subdomain)
    {
        return await _context.Tenants
            .FirstOrDefaultAsync(t => t.Subdomain == subdomain);
    }
}