using EduHR.Domain.Common;
using EduHR.Domain.Interfaces;
using System;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir personelin belirli bir dönemdeki maaş bilgisini temsil eder.
/// </summary>
public class Salary : AuditableEntity, ITenantEntity
{
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    /// <summary>
    /// Bu maaş bilgisinin ait olduğu personel.
    /// </summary>
    public int PersonnelId { get; set; }
    public Personnel Personnel { get; set; } = null!;
    
    /// <summary>
    /// Brüt maaş tutarı.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Bu maaşın geçerli olmaya başladığı tarih.
    /// </summary>
    public DateTime EffectiveDate { get; set; }
}