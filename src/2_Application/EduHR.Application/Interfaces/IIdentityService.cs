using EduHR.Common.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHR.Application.Interfaces;

/// <summary>
/// Provides an abstraction for managing users and roles, decoupling the application layer from the specific identity implementation.
/// Kimlik yönetimi (Identity) işlemleri için sözleşmeyi tanımlar.
/// </summary>
public interface IIdentityService
{
    /// <summary>
    /// Creates a new user in the system and assigns them to the specified roles.
    /// Yeni bir kullanıcı oluşturur ve belirtilen rolleri atar.
    /// </summary>
    /// <param name="tenantId">The ID of the tenant the user belongs to. / Kullanıcının ait olduğu kiracı kimliği.</param>
    /// <param name="firstName">The user's first name. / Kullanıcının adı.</param>
    /// <param name="lastName">The user's last name. / Kullanıcının soyadı.</param>
    /// <param name="email">The user's email address. / Kullanıcının e-posta adresi.</param>
    /// <param name="password">The user's password. / Kullanıcının şifresi.</param>
    /// <param name="roles">A list of roles to assign to the user. / Kullanıcıya atanacak rollerin listesi.</param>
    /// <returns>A tuple containing the operation result and the ID of the created user. / İşlem sonucu ve oluşturulan kullanıcının kimliğini içeren bir Tuple.</returns>
    Task<(ApiResponse Result, int UserId)> CreateUserAsync(int tenantId, string firstName, string lastName, string email, string password, List<string> roles);
    
    // Gelecekte eklenebilecek diğer metotlar:
    // Task<ApiResponse> AddToRoleAsync(int userId, string role);
    // Task<bool> IsInRoleAsync(int userId, string role);
    // Task<UserDto?> FindUserByEmailAsync(string email);
}