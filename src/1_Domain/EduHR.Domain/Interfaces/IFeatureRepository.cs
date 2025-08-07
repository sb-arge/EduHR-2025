using EduHR.Domain.Entities;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Feature varlığı için veri erişim sözleşmesi.
/// </summary>
public interface IFeatureRepository : IGenericRepository<Feature>
{
    /// <summary>
    /// Benzersiz özellik koduna göre bir özelliği getirir.
    /// </summary>
    /// <param name="featureCode">Özelliğin programatik kodu.</param>
    /// <returns>Bulunursa Feature nesnesi, bulunamazsa null.</returns>
    Task<Feature?> GetByCodeAsync(string featureCode);
}