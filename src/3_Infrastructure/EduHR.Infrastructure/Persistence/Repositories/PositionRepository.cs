using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the repository for the Position entity.
/// </summary>
public class PositionRepository : GenericRepository<Position>, IPositionRepository
{
    public PositionRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves all positions belonging to a specific department.
    /// </summary>
    public async Task<IEnumerable<Position>> GetAllByDepartmentIdAsync(int departmentId)
    {
        return await _context.Positions
            .Where(p => p.DepartmentId == departmentId)
            .OrderBy(p => p.Title)
            .ToListAsync();
    }
    
    /// <summary>
    /// Retrieves all positions belonging to a specific tenant.
    /// </summary>
    public async Task<IEnumerable<Position>> GetAllByTenantIdAsync(int tenantId)
    {
        // Not: DbContext'teki global filtre bunu zaten yapıyor, ancak bu metot,
        // gelecekte global filtrenin olmadığı senaryolarda veya daha karmaşık
        // sorgularda güvenliği sağlamak için yine de gereklidir.
        return await _context.Positions
            .Where(p => p.TenantId == tenantId)
            .OrderBy(p => p.Title)
            .ToListAsync();
    }
}