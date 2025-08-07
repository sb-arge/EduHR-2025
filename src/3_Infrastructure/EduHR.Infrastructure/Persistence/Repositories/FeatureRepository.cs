using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the repository for the Feature entity.
/// </summary>
public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
{
    public FeatureRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a feature by its unique code.
    /// </summary>
    /// <param name="featureCode">The feature's programmatic code.</param>
    /// <returns>The feature if found; otherwise, null.</returns>
    public async Task<Feature?> GetByCodeAsync(string featureCode)
    {
        return await _context.Features
            .FirstOrDefaultAsync(f => f.FeatureCode == featureCode);
    }
}