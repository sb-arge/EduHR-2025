using AutoMapper;
using EduHR.Application.Features.Features.Queries;
using EduHR.Common.DTOs;
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Features.Handlers;

/// <summary>
/// Handles the GetAllFeaturesQuery.
/// </summary>
public class GetAllFeaturesQueryHandler : IRequestHandler<GetAllFeaturesQuery, PagedResultDto<FeatureDto>>
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IMapper _mapper;

    public GetAllFeaturesQueryHandler(IFeatureRepository featureRepository, IMapper mapper)
    {
        _featureRepository = featureRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<FeatureDto>> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
    {
        var features = await _featureRepository.GetAllAsync(); // Sayfalama eklenecek
        var featureDtos = _mapper.Map<List<FeatureDto>>(features);

        return new PagedResultDto<FeatureDto>
        {
            Items = featureDtos,
            TotalCount = featureDtos.Count, // Gerçek uygulamada ayrı sorgu ile alınacak
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}