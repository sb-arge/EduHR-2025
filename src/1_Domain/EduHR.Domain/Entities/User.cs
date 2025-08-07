using EduHR.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EduHR.Domain.Entities;

/// <summary>
/// Sisteme giriş yapacak bir kullanıcıyı temsil eder.
/// </summary>
public class User : IdentityUser<int>, ITenantEntity
{
    /// <summary>
    /// Bu kullanıcının ait olduğu kiracının kimliği.
    /// </summary>
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    
    /// <summary>
    /// Kullanıcının adı.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcının soyadı.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Kullanıcının aktif olup olmadığını belirtir.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Eğer bu kullanıcı bir personel ise, ilgili personel kaydına referans.
    /// </summary>
    public int? PersonnelId { get; set; }
    public Personnel? Personnel { get; set; }
}