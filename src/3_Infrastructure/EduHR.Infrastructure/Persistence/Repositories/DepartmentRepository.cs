using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the repository for the Department entity.
/// </summary>
public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves all departments for a specific tenant in a hierarchical structure.
    /// </summary>
    public async Task<IEnumerable<Department>> GetAllByTenantIdAsHierarchyAsync(int tenantId)
    {
        // Not: Bu metot, tüm departmanları çekip bellekte hiyerarşiye dönüştürür.
        // Çok büyük organizasyonlar için performansı artırmak adına "recursive CTE" gibi
        // veritabanı-seviyesi hiyerarşik sorgular da değerlendirilebilir.
        var allDepartments = await _context.Departments
            .Where(d => d.TenantId == tenantId)
            .ToListAsync();

        return allDepartments
            .Where(d => d.ParentDepartmentId == null) // Sadece en üst seviye (kök) departmanları seç
            .ToList();
    }
    
    /// <summary>
    /// Checks if a department has any active personnel or sub-departments.
    /// </summary>
    public async Task<bool> HasActivePersonnelOrSubDepartmentsAsync(int departmentId)
    {
        var hasSubDepartments = await _context.Departments.AnyAsync(d => d.ParentDepartmentId == departmentId);
        if (hasSubDepartments) return true;

        var hasPersonnel = await _context.Set<Personnel>().AnyAsync(p => p.Position.DepartmentId == departmentId); // Personnel DbSet'i eklenince çalışacak
        if (hasPersonnel) return true;

        return false;
    }
}