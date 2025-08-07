using EduHR.Application.Exceptions;
using EduHR.Application.Features.Features.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Features.Handlers;

public class DeleteFeatureCommandHandler : IRequestHandler<DeleteFeatureCommand>
{
    private readonly IFeatureRepository _featureRepository;

    public DeleteFeatureCommandHandler(IFeatureRepository featureRepository)
    {
        _featureRepository = featureRepository;
    }

    public async Task Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
    {
        var featureToDelete = await _featureRepository.GetByIdAsync(request.Id);

        if (featureToDelete is null)
        {
            throw new NotFoundException(nameof(Feature), request.Id);
        }

        // TODO: Bu özelliğin herhangi bir plana bağlı olup olmadığını kontrol et.
        // Varsa, EntityCannotBeDeletedException fırlat.

        _featureRepository.Delete(featureToDelete);
    }
}