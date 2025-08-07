using EduHR.Domain.Common;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir abonelik planına dahil edilebilecek bir özelliği temsil eder.
/// </summary>
public class Feature : AuditableEntity
{
    /// <summary>
    /// Özelliğin programatik kodu (örn: "recruitment_module").
    /// </summary>
    public string FeatureCode { get; set; } = string.Empty;

    /// <summary>
    /// Bu özelliği içeren planların listesi (Çoka-Çok ilişki).
    /// </summary>
    public ICollection<Plan> Plans { get; set; } = new List<Plan>();

    /// <summary>
    /// Bu özelliğin çok dilli çevirileri.
    /// </summary>
    public ICollection<FeatureTranslation> Translations { get; set; } = new List<FeatureTranslation>();
}