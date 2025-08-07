using EduHR.Application.Exceptions;
using EduHR.Application.Features.Plans.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Plans.Handlers;

/// <summary>
/// Handles the update of a Plan's associated Features.
/// </summary>
public class UpdatePlanFeaturesCommandHandler : IRequestHandler<UpdatePlanFeaturesCommand>
{
    private readonly IPlanRepository _planRepository;
    private readonly IFeatureRepository _featureRepository;

    public UpdatePlanFeaturesCommandHandler(IPlanRepository planRepository, IFeatureRepository featureRepository)
    {
        _planRepository = planRepository;
        _featureRepository = featureRepository;
    }

    public async Task Handle(UpdatePlanFeaturesCommand request, CancellationToken cancellationToken)
    {
        // İlgili Plan'ı, mevcut özellikleri ile birlikte veritabanından getir.
        var planToUpdate = await _planRepository.GetByIdWithFeaturesAsync(request.PlanId);
        if (planToUpdate is null)
        {
            throw new NotFoundException(nameof(Plan), request.PlanId);
        }

        // Komutta gelen yeni Feature ID'lerine karşılık gelen Feature varlıklarını bul.
        var newFeatures = new List<Feature>();
        if (request.FeatureIds.Any())
        {
            foreach (var featureId in request.FeatureIds)
            {
                var feature = await _featureRepository.GetByIdAsync(featureId);
                if (feature is not null)
                {
                    newFeatures.Add(feature);
                }
                // Opsiyonel: Eğer bir featureId bulunamazsa hata fırlatılabilir.
            }
        }

        // Plan'ın mevcut özellik listesini temizle ve yenilerini ekle.
        planToUpdate.Features.Clear();
        foreach (var feature in newFeatures)
        {
            planToUpdate.Features.Add(feature);
        }

        _planRepository.Update(planToUpdate);
    }
}