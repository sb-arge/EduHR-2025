using EduHR.Domain.Common;
using EduHR.Domain.Interfaces;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir kiracıya ait departmanı temsil eder.
/// </summary>
public class Department : AuditableEntity, ITenantEntity
{
    /// <summary>
    /// Bu departmanın ait olduğu kiracının kimliği.
    /// </summary>
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    /// <summary>
    /// Departmanın programatik adı veya kodu (örn: "IK_DEPT").
    /// Arayüzde gösterilecek olan çeviriler Translation tablosunda tutulur.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Varsa, bu departmanın bağlı olduğu üst departmanın kimliği.
    /// </summary>
    public int? ParentDepartmentId { get; set; }

    /// <summary>
    /// Varsa, bu departmanın üst departmanı (Navigation Property).
    /// </summary>
    public Department? ParentDepartment { get; set; }

    /// <summary>
    /// Bu departmana bağlı alt departmanlar (Navigation Property).
    /// </summary>
    public ICollection<Department> SubDepartments { get; set; } = new List<Department>();

    /// <summary>
    /// Bu departmanda bulunan pozisyonlar (Navigation Property).
    /// </summary>
    public ICollection<Position> Positions { get; set; } = new List<Position>();

    /// <summary>
    /// Departman adının çok dilli çevirileri (Navigation Property).
    /// </summary>
    public ICollection<DepartmentTranslation> Translations { get; set; } = new List<DepartmentTranslation>();
}