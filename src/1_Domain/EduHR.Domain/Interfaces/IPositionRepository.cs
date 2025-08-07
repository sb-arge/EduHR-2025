using EduHR.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Position varlığı için veri erişim sözleşmesi.
/// </summary>
public interface IPositionRepository : IGenericRepository<Position>
{
    /// <summary>
    /// Belirli bir departmana ait tüm pozisyonları getirir.
    /// </summary>
    /// <param name="departmentId">Departmanın kimliği.</param>
    /// <returns>Pozisyon listesi.</returns>
    Task<IEnumerable<Position>> GetAllByDepartmentIdAsync(int departmentId);
    
    /// <summary>
    /// Belirli bir kiracıya ait tüm pozisyonları getirir.
    /// </summary>
    /// <param name="tenantId">Kiracının kimliği.</param>
    /// <returns>Pozisyon listesi.</returns>
    Task<IEnumerable<Position>> GetAllByTenantIdAsync(int tenantId);
}