using EduHR.Domain.Common;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir Departmanın çok dilli çevirilerini barındırır.
/// </summary>
public class DepartmentTranslation : BaseEntity
{
    /// <summary>
    /// Ana Department varlığına referans.
    /// </summary>
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    /// <summary>
    /// Çevirinin dil kodu (örn: "tr-TR", "en-US").
    /// </summary>
    public string LanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// Departmanın çevrilmiş adı.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Departmanın çevrilmiş açıklaması (isteğe bağlı).
    /// </summary>
    public string? Description { get; set; }
}