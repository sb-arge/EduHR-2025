using EduHR.Domain.Common;
using EduHR.Domain.Enums;

namespace EduHR.Domain.Entities;

/// <summary>
/// Satılabilir bir abonelik planını temsil eder.
/// </summary>
public class Plan : AuditableEntity
{
    /// <summary>
    /// Planın programatik adı (örn: "basic_monthly").
    /// </summary>
    public string PlanCode { get; set; } = string.Empty;

    /// <summary>
    /// Planın fiyatı.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Faturalandırma döngüsü (Aylık/Yıllık).
    /// </summary>
    public BillingCycle BillingCycle { get; set; }

    /// <summary>
    /// Bu planın izin verdiği maksimum kullanıcı sayısı.
    /// </summary>
    public int UserLimit { get; set; }

    /// <summary>
    /// Bu plana dahil olan özelliklerin listesi.
    /// </summary>
    public ICollection<Feature> Features { get; set; } = new List<Feature>();

    /// <summary>
    /// Bu planın çok dilli çevirileri.
    /// </summary>
    public ICollection<PlanTranslation> Translations { get; set; } = new List<PlanTranslation>();
}