using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the repository for the Subscription entity.
/// </summary>
public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves the current active subscription for a specific tenant.
    /// </summary>
    public async Task<Subscription?> GetCurrentActiveSubscriptionByTenantIdAsync(int tenantId)
    {
        return await _context.Subscriptions
            .Include(s => s.Plan) // Plan bilgilerini de dahil et
            .Where(s => s.TenantId == tenantId && s.Status == Domain.Enums.SubscriptionStatus.Active)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves all historical and current subscriptions for a specific tenant.
    /// </summary>
    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsByTenantIdAsync(int tenantId)
    {
        return await _context.Subscriptions
            .Where(s => s.TenantId == tenantId)
            .OrderByDescending(s => s.StartDate)
            .ToListAsync();
    }
    
    /// <summary>
    /// Checks if there are any active subscriptions for a specific plan.
    /// </summary>
    public async Task<bool> AnyActiveByPlanIdAsync(int planId)
    {
        return await _context.Subscriptions
            .AnyAsync(s => s.PlanId == planId && s.Status == Domain.Enums.SubscriptionStatus.Active);
    }
}