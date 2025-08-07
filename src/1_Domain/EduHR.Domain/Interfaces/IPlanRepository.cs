using EduHR.Domain.Entities;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Plan varlığı için veri erişim sözleşmesi.
/// </summary>
public interface IPlanRepository : IGenericRepository<Plan>
{
    /// <summary>
    /// Benzersiz plan koduna göre bir planı getirir.
    /// </summary>
    /// <param name="planCode">Planın programatik kodu.</param>
    /// <returns>Bulunursa Plan nesnesi, bulunamazsa null.</returns>
    Task<Plan?> GetByCodeAsync(string planCode);

    /// <summary>
    /// Bir planı, içerdiği tüm özelliklerle (Features) birlikte getirir.
    /// </summary>
    /// <param name="planId">Planın kimliği.</param>
    /// <returns>Bulunursa özellikleri yüklenmiş Plan nesnesi, bulunamazsa null.</returns>
    Task<Plan?> GetByIdWithFeaturesAsync(int planId);
}