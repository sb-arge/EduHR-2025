using EduHR.Domain.Entities;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Department varlığı için veri erişim sözleşmesi.
/// </summary>
public interface IDepartmentRepository : IGenericRepository<Department>
{
    /// <summary>
    /// Bir kiracıya ait tüm departmanları, hiyerarşik bir yapıda (alt departmanlarıyla birlikte) getirir.
    /// </summary>
    /// <param name="tenantId">Kiracının kimliği.</param>
    /// <returns>Departmanların kök listesi.</returns>
    Task<IEnumerable<Department>> GetAllByTenantIdAsHierarchyAsync(int tenantId);

    /// <summary>
    /// Belirtilen bir departmana bağlı aktif personel veya alt departman olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="departmentId">Departmanın kimliği.</param>
    /// <returns>Bağlı kayıt varsa true, yoksa false döner.</returns>
    Task<bool> HasActivePersonnelOrSubDepartmentsAsync(int departmentId);
}