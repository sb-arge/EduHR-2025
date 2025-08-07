using EduHR.Domain.Common;
using EduHR.Domain.Enums;
using EduHR.Domain.Interfaces;
using System;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir personelin izin talebini temsil eder.
/// </summary>
public class LeaveRequest : AuditableEntity, ITenantEntity
{
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    /// <summary>
    /// Talebi oluşturan personel.
    /// </summary>
    public int PersonnelId { get; set; }
    public Personnel Personnel { get; set; } = null!;
    
    /// <summary>
    /// İzin türü (Yıllık, Mazeret vb.).
    /// </summary>
    public int LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public RequestStatus Status { get; set; }
    public string? Reason { get; set; } // Talep nedeni
    public string? RejectionReason { get; set; } // Reddedilme nedeni
}