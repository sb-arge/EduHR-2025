using EduHR.Domain.Entities;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Tenant varlığı için veri erişim sözleşmesi.
/// </summary>
public interface ITenantRepository : IGenericRepository<Tenant>
{
    // Bu arayüze, sadece Tenant'a özgü olan sorgu metotları ekleyebiliriz.
    // Örneğin:
    Task<Tenant?> GetBySubdomainAsync(string subdomain);
}