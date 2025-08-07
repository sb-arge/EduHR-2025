using Microsoft.AspNetCore.Identity;

namespace EduHR.Domain.Entities;

/// <summary>
/// Bir kullanıcı rolünü temsil eder.
/// </summary>
public class Role : IdentityRole<int>
{
    /// <summary>
    /// Rolün amacını açıklayan bir metin.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Bu rolün bir kiracıya özel olup olmadığını belirtir.
    /// Null ise, bu bir sistem rolüdür.
    /// Dolu ise, sadece o kiracıya ait özel bir roldür.
    /// </summary>
    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
}