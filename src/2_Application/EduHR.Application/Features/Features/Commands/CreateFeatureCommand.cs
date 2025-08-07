using EduHR.Common.DTOs;
using MediatR;

namespace EduHR.Application.Features.Features.Commands;

/// <summary>
/// Represents the command to create a new Feature.
/// </summary>
public class CreateFeatureCommand : IRequest<CreateFeatureCommandResponse>
{
    public string FeatureCode { get; set; } = string.Empty;
    public ICollection<FeatureTranslationDto> Translations { get; set; } = new List<FeatureTranslationDto>();
}

// Bu DTO'nun EduHR.Common/DTOs/FeatureDtos.cs gibi bir dosyada olması best practice'dir.
public class FeatureTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateFeatureCommandResponse
{
    public int Id { get; set; }
    public string FeatureCode { get; set; } = string.Empty;
}