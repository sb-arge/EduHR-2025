using EduHR.Domain.Common;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir Pozisyonun çok dilli çevirilerini barındırır.
/// </summary>
public class PositionTranslation : BaseEntity
{
    public int PositionId { get; set; }
    public Position Position { get; set; } = null!;

    public string LanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// Pozisyonun çevrilmiş başlığı.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Pozisyonun çevrilmiş açıklaması (isteğe bağlı).
    /// </summary>
    public string? Description { get; set; }
}