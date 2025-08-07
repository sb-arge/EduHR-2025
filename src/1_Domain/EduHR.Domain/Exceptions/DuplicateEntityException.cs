namespace EduHR.Domain.Exceptions;

/// <summary>
/// Sistemde benzersiz olması gereken bir kayıt oluşturulmaya çalışıldığında fırlatılan hata.
/// </summary>
public class DuplicateEntityException : DomainException
{
    public DuplicateEntityException(string message) : base(message)
    {
    }

    /// <summary>
    /// Daha açıklayıcı bir hata mesajı oluşturmak için bir yardımcı metot.
    /// </summary>
    /// <param name="entityName">Varlığın adı (örn: "Kullanıcı")</param>
    /// <param name="key">Benzersiz alanın adı (örn: "E-posta")</param>
    /// <param name="value">Tekrar eden değer</param>
    /// <returns>Yeni bir DuplicateEntityException nesnesi.</returns>
    public static DuplicateEntityException ForEntity(string entityName, string key, object value)
    {
        return new DuplicateEntityException($"'{value}' {key} değerine sahip bir '{entityName}' zaten mevcut.");
    }
}