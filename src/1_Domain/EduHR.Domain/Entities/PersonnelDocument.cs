using EduHR.Domain.Common;
using EduHR.Domain.Interfaces;

namespace EduHR.Domain.Entities;

/// <summary>
/// Personele ait bir dokümanı temsil eder.
/// </summary>
public class PersonnelDocument : AuditableEntity, ITenantEntity
{
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    /// <summary>
    /// Bu dokümanın ait olduğu personel.
    /// </summary>
    public int PersonnelId { get; set; }
    public Personnel Personnel { get; set; } = null!;

    /// <summary>
    /// Dokümanın adı (örn: "Lisans Diploması.pdf").
    /// </summary>
    public string DocumentName { get; set; } = string.Empty;

    /// <summary>
    /// Dokümanın depolandığı yerin URL'i veya yolu.
    /// </summary>
    public string DocumentUrl { get; set; } = string.Empty;

    /// <summary>
    /// Doküman türü (örn: "Diploma", "Sözleşme").
    /// </summary>
    public string DocumentType { get; set; } = string.Empty;
}