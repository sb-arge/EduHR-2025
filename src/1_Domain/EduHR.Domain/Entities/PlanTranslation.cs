using EduHR.Domain.Common;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir Planın çok dilli çevirilerini barındırır.
/// </summary>
public class PlanTranslation : BaseEntity
{
    /// <summary>
    /// Ana Plan varlığına referans.
    /// </summary>
    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;

    /// <summary>
    /// Çevirinin dil kodu (örn: "tr-TR", "en-US").
    /// </summary>
    public string LanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// Planın çevrilmiş adı.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Planın çevrilmiş açıklaması (isteğe bağlı).
    /// </summary>
    public string? Description { get; set; }
}