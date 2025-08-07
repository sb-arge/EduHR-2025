namespace EduHR.Domain.Common;

/// <summary>
/// Sistemdeki tüm varlıklar için temel sınıfı temsil eder.
/// Tüm varlıklar için ortak bir tanımlayıcı (Id) sağlar.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Varlığın benzersiz kimliği.
    /// </summary>
    public int Id { get; set; }
}