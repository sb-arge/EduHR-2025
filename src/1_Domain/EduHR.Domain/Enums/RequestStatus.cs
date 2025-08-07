namespace EduHR.Domain.Enums;

/// <summary>
/// İzin, avans gibi taleplerin durumunu belirtir.
/// </summary>
public enum RequestStatus
{
    /// <summary>
    /// Talep oluşturuldu, onay bekliyor.
    /// </summary>
    Pending,

    /// <summary>
    /// Talep onaylandı.
    /// </summary>
    Approved,

    /// <summary>
    /// Talep reddedildi.
    /// </summary>
    Rejected,

    /// <summary>
    /// Talep, oluşturan kişi tarafından iptal edildi.
    /// </summary>
    Cancelled
}