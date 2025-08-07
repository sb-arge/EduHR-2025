namespace EduHR.Domain.Common;

/// <summary>
/// Denetlenebilir, durumu yönetilebilir ve "soft-delete" edilebilir bir varlığı temsil eder.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    // Oluşturma Bilgileri
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }

    // Güncelleme Bilgileri
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }

    // Silme Bilgileri (Soft Delete için)
    public DateTime? DeletedDate { get; set; }
    public string? DeletedBy { get; set; }

    // Durum Yönetimi
    public bool IsActive { get; set; } = true; // Varsayılan olarak aktif başlasın
    public bool IsArchived { get; set; } = false;
}