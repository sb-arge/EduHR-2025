namespace EduHR.Domain.Enums;

public enum SubscriptionStatus
{
    /// <summary>
    /// Abonelik ödeme bekliyor veya aktif değil.
    /// </summary>
    Pending,

    /// <summary>
    /// Abonelik aktif ve kullanımda.
    /// </summary>
    Active,

    /// <summary>
    /// Abonelik süresi dolmuş.
    /// </summary>
    Expired,

    /// <summary>
    /// Abonelik iptal edilmiş.
    /// </summary>
    Cancelled
}