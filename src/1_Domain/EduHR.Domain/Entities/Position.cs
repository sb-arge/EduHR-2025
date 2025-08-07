using EduHR.Domain.Common;
using EduHR.Domain.Interfaces;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir departmana bağlı pozisyonu/unvanı temsil eder.
/// </summary>
public class Position : AuditableEntity, ITenantEntity
{
    /// <summary>
    /// Bu pozisyonun ait olduğu kiracının kimliği.
    /// </summary>
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    /// <summary>
    /// Bu pozisyonun ait olduğu departmanın kimliği.
    /// </summary>
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    
    /// <summary>
    /// Pozisyonun programatik adı veya unvanı (örn: "SENIOR_SOFTWARE_DEV").
    /// Arayüzde gösterilecek olan çeviriler Translation tablosunda tutulur.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Pozisyon başlığının çok dilli çevirileri (Navigation Property).
    /// </summary>
    public ICollection<PositionTranslation> Translations { get; set; } = new List<PositionTranslation>();
    
    /// <summary>
    /// Bu pozisyondaki personeller (Navigation Property).
    /// </summary>
    public ICollection<Personnel> Personnel { get; set; } = new List<Personnel>();
}