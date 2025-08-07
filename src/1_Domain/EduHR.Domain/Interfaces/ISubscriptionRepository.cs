using EduHR.Domain.Entities;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Subscription varlığı için veri erişim sözleşmesi.
/// </summary>
public interface ISubscriptionRepository : IGenericRepository<Subscription>
{
    /// <summary>
    /// Belirtilen bir kiracının o anki aktif olan aboneliğini getirir.
    /// </summary>
    /// <param name="tenantId">Kiracının kimliği.</param>
    /// <returns>Aktif abonelik nesnesi.</returns>
    Task<Subscription?> GetCurrentActiveSubscriptionByTenantIdAsync(int tenantId);

    /// <summary>
    /// Belirtilen bir kiracının geçmiş ve güncel tüm aboneliklerini listeler.
    /// </summary>
    /// <param name="tenantId">Kiracının kimliği.</param>
    /// <returns>Abonelik listesi.</returns>
    Task<IEnumerable<Subscription>> GetAllSubscriptionsByTenantIdAsync(int tenantId);

    /// <summary>
    /// Belirtilen bir plana ait, aktif durumda olan herhangi bir abonelik olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="planId">Planın kimliği.</param>
    /// <returns>Aktif abonelik varsa true, yoksa false döner.</returns>
    Task<bool> AnyActiveByPlanIdAsync(int planId);
}