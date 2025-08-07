using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the repository for the Plan entity.
/// </summary>
public class PlanRepository : GenericRepository<Plan>, IPlanRepository
{
    public PlanRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a plan by its unique code.
    /// </summary>
    public async Task<Plan?> GetByCodeAsync(string planCode)
    {
        return await _context.Plans
            .FirstOrDefaultAsync(p => p.PlanCode == planCode);
    }

    /// <summary>
    /// Retrieves a plan including its associated features.
    /// </summary>
    public async Task<Plan?> GetByIdWithFeaturesAsync(int planId)
    {
        return await _context.Plans
            .Include(p => p.Features)
            .FirstOrDefaultAsync(p => p.Id == planId);
    }
}