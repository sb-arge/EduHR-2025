using EduHR.Common.DTOs; // DepartmentDto için
using MediatR;

namespace EduHR.Application.Features.Departments.Commands;

/// <summary>
/// Yeni bir departman oluşturmak için komutu temsil eder.
/// </summary>
public class CreateDepartmentCommand : IRequest<DepartmentDto>
{
    /// <summary>
    /// Departmanın programatik adı.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Varsa, üst departmanın kimliği.
    /// </summary>
    public int? ParentDepartmentId { get; set; }

    // Not: Çok dilli çevirileri yönetmek için ayrı bir DTO koleksiyonu da eklenebilir.
    // public ICollection<DepartmentTranslationDto> Translations { get; set; } = new List<DepartmentTranslationDto>();
}
