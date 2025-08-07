using EduHR.Domain.Common;
using EduHR.Domain.Interfaces;
using System.Collections.Generic;

namespace EduHR.Domain.Entities;

/// <summary>
/// Kiracı tarafından tanımlanabilen bir izin türünü temsil eder.
/// </summary>
public class LeaveType : AuditableEntity, ITenantEntity
{
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    /// <summary>
    /// İzin türünün programatik adı (örn: "ANNUAL_LEAVE").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Bu izin türü için varsayılan olarak atanan gün sayısı.
    /// </summary>
    public int DefaultDays { get; set; }

    /// <summary>
    /// İzin türü adının çok dilli çevirileri.
    /// </summary>
    public ICollection<LeaveTypeTranslation> Translations { get; set; } = new List<LeaveTypeTranslation>();
}