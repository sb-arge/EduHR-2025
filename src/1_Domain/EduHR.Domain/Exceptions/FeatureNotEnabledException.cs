namespace EduHR.Domain.Exceptions;

/// <summary>
/// Bir kiracı, mevcut abonelik planında bulunmayan bir özelliği kullanmaya çalıştığında fırlatılan hata.
/// </summary>
public class FeatureNotEnabledException : DomainException
{
    public FeatureNotEnabledException(string featureName)
        : base($"'{featureName}' özelliği mevcut abonelik planınızda bulunmamaktadır. Lütfen planınızı yükseltin.")
    {
    }
}