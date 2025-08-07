using AutoMapper;
using EduHR.Application.Features.Features.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Exceptions;
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Features.Handlers;

/// <summary>
/// Handles the creation of a new Feature.
/// </summary>
public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, CreateFeatureCommandResponse>
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IMapper _mapper;

    public CreateFeatureCommandHandler(IFeatureRepository featureRepository, IMapper mapper)
    {
        _featureRepository = featureRepository;
        _mapper = mapper;
    }

    public async Task<CreateFeatureCommandResponse> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
    {
        var existingFeature = await _featureRepository.GetByCodeAsync(request.FeatureCode);
        if (existingFeature is not null)
        {
            throw DuplicateEntityException.ForEntity("Feature", "Feature Code", request.FeatureCode);
        }

        var newFeature = _mapper.Map<Feature>(request);

        await _featureRepository.AddAsync(newFeature);

        return _mapper.Map<CreateFeatureCommandResponse>(newFeature);
    }
}