using EduHR.Domain.Common;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir İzin Türünün çok dilli çevirilerini barındırır.
/// </summary>
public class LeaveTypeTranslation : BaseEntity
{
    public int LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; } = null!;

    public string LanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// İzin türünün çevrilmiş adı.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// İzin türünün çevrilmiş açıklaması (isteğe bağlı).
    /// </summary>
    public string? Description { get; set; }
}