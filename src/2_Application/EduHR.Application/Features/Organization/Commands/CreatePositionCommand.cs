using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Organization.Commands;

/// <summary>
/// Yeni bir pozisyon oluşturmak için komutu temsil eder.
/// </summary>
public class CreatePositionCommand : IRequest<PositionDto>
{
    /// <summary>
    /// Pozisyonun başlığı (örn: "Kıdemli Yazılım Geliştirici").
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Bu pozisyonun bağlı olduğu departmanın kimliği.
    /// </summary>
    public int DepartmentId { get; set; }

    // Not: Çok dilli çeviriler de buraya eklenebilir.
}