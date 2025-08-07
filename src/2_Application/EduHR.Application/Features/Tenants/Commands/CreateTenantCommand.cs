using MediatR;

namespace EduHR.Application.Features.Tenants.Commands;

/// <summary>
/// Yeni bir Kiracı, ilk Yönetici Kullanıcısı ve ilk Aboneliği oluşturmak için komutu temsil eder.
/// </summary>
public class CreateTenantCommand : IRequest<CreateTenantCommandResponse>
{
    // Kiracı Bilgileri
    public string CompanyName { get; set; } = string.Empty;
    public string Subdomain { get; set; } = string.Empty;

    // İlk Yönetici Kullanıcı Bilgileri
    public string AdminFirstName { get; set; } = string.Empty;
    public string AdminLastName { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;

    // İlk Abonelik Bilgileri
    public int PlanId { get; set; }
}

public class CreateTenantCommandResponse
{
    public int TenantId { get; set; }
    public int AdminUserId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
}