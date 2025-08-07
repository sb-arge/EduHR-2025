namespace EduHR.Domain.Interfaces;

/// <summary>
/// Bir varlığın kiracıya özel olduğunu ve TenantId taşıması gerektiğini belirtir.
/// </summary>
public interface ITenantEntity
{
    public int TenantId { get; set; }
}