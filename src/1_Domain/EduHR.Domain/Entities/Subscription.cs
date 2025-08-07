using EduHR.Domain.Common;
using EduHR.Domain.Enums;
using EduHR.Domain.Interfaces;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir kiracının bir plana olan aboneliğini temsil eder.
/// </summary>
public class Subscription : AuditableEntity, ITenantEntity
{
    // Kiracı ve Plan İlişkileri
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;

    // Abonelik Süresi ve Durumu
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionStatus Status { get; set; }
    
    // --- YENİ EKLENEN FİNANSAL ANLIK GÖRÜNTÜ ALANLARI ---

    /// <summary>
    /// Aboneliğin oluşturulduğu andaki fatura döngüsü (Aylık/Yıllık).
    /// Planın gelecekteki döngüsü değişse bile bu kayıt sabit kalır.
    /// </summary>
    public BillingCycle BillingCycleAtTimeOfSubscription { get; set; }

    /// <summary>
    /// Aboneliğin oluşturulduğu andaki birim fiyat.
    /// Planın gelecekteki fiyatı değişse bile bu kayıt sabit kalır.
    /// </summary>
    public decimal PriceAtTimeOfSubscription { get; set; }
}