using MediatR;

namespace EduHR.Application.Features.Features.Commands;

/// <summary>
/// Mevcut bir Feature'ı güncellemek için komutu temsil eder.
/// </summary>
public class UpdateFeatureCommand : IRequest
{
    public int Id { get; set; }
    public ICollection<FeatureTranslationDto> Translations { get; set; } = new List<FeatureTranslationDto>();
}