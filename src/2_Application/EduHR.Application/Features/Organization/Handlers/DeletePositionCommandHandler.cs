using EduHR.Application.Exceptions;
using EduHR.Application.Features.Organization.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Organization.Handlers;

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand>
{
    private readonly IPositionRepository _positionRepository;

    public DeletePositionCommandHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var positionToDelete = await _positionRepository.GetByIdAsync(request.Id);

        if (positionToDelete is null)
        {
            throw new NotFoundException(nameof(Position), request.Id);
        }

        // --- İŞ KURALI KONTROLÜ ---
        // TODO: Bu pozisyona atanmış aktif bir personel olup olmadığını kontrol et.
        // Varsa, EntityCannotBeDeletedException fırlat.

        _positionRepository.Delete(positionToDelete);
    }
}