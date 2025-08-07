using EduHR.Domain.Common;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir Feature'ın çok dilli çevirilerini barındırır.
/// </summary>
public class FeatureTranslation : BaseEntity
{
    public int FeatureId { get; set; }
    public Feature Feature { get; set; } = null!;

    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}