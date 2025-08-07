namespace EduHR.Domain.Exceptions;

/// <summary>
/// Bir kiracı, abonelik planında tanımlanan kullanıcı limitini aşmaya çalıştığında fırlatılan hata.
/// </summary>
public class UserLimitExceededException : DomainException
{
    public UserLimitExceededException(int limit)
        : base($"Yeni kullanıcı eklenemiyor. Bu abonelik planı için belirlenen {limit} kullanıcı limitine ulaşıldı.")
    {
    }
}