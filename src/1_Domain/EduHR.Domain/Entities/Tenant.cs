using EduHR.Domain.Common;

namespace EduHR.Domain.Entities;

/// <summary>
/// Sisteme abone olan bir kurumu (kiracıyı) temsil eder.
/// </summary>
public class Tenant : AuditableEntity
{
    /// <summary>
    /// Kiracının yasal veya ticari adı.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Kiracının veritabanında benzersiz olmasını sağlayan bir alt alan adı veya kod.
    /// </summary>
    public string Subdomain { get; set; } = string.Empty;
    
    // Diğer kurumsal bilgiler eklenebilir (adres, telefon, vergi no vb.)

    /// <summary>
    /// Bu kiracıya ait aboneliklerin listesi.
    /// </summary>
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    
    /// <summary>
    /// Bu kiracıya ait kullanıcıların listesi.
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}