using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Organization.Commands;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Organization.Handlers;

/// <summary>
/// Handles the UpdatePositionCommand.
/// </summary>
public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, PositionDto>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;

    public UpdatePositionCommandHandler(IPositionRepository positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionDto> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var positionToUpdate = await _positionRepository.GetByIdAsync(request.Id);

        if (positionToUpdate is null)
        {
            throw new NotFoundException(nameof(Position), request.Id);
        }

        // TODO: Güvenlik kontrolü - Bu pozisyonun, işlemi yapan kullanıcının kiracısına ait olup olmadığını doğrula.

        _mapper.Map(request, positionToUpdate);
        _positionRepository.Update(positionToUpdate);
        
        return _mapper.Map<PositionDto>(positionToUpdate);
    }
}