using EduHR.Domain.Entities;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// User varlığı için veri erişim sözleşmesi.
/// Bu arayüz, Identity tarafından yönetildiği için IGenericRepository'den miras almaz.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Belirtilen bir kiracıya ait tüm kullanıcıları listeler.
    /// </summary>
    Task<IEnumerable<User>> GetAllByTenantIdAsync(int tenantId);

    /// <summary>
    /// Bir kullanıcıyı kimliğine göre bulur.
    /// </summary>
    Task<User?> GetByIdAsync(int userId);

    /// <summary>
    /// Bir kullanıcıyı, sahip olduğu tüm rollerle birlikte getirir.
    /// </summary>
    Task<User?> GetByIdWithRolesAsync(int userId);

    // Not: Add, Update, Delete gibi işlemler, ASP.NET Identity'nin kendi
    // UserManager servisi üzerinden yapılacağı için burada ayrıca tanımlanması gerekmez.
    // Bu repository daha çok okuma (read) operasyonlarına odaklanacaktır.
}