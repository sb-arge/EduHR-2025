using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Personnel.Commands;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Events;
using EduHR.Domain.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

// --- HATA ÇÖZÜMÜ: Domain'deki Personnel sınıfına bir takma ad veriyoruz. ---
using PersonnelEntity = EduHR.Domain.Entities.Personnel;

namespace EduHR.Application.Features.Personnel.Handlers;

/// <summary>
/// Handles the CreatePersonnelCommand.
/// </summary>
public class CreatePersonnelCommandHandler : IRequestHandler<CreatePersonnelCommand, PersonnelSummaryDto>
{
    private readonly IPersonnelRepository _personnelRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreatePersonnelCommandHandler(
        IPersonnelRepository personnelRepository,
        IPositionRepository positionRepository,
        IIdentityService identityService,
        ICurrentUserService currentUserService,
        IMapper mapper,
        IMediator mediator)
    {
        _personnelRepository = personnelRepository;
        _positionRepository = positionRepository;
        _identityService = identityService;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<PersonnelSummaryDto> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        var position = await _positionRepository.GetByIdAsync(request.PositionId);
        if (position is null || position.TenantId != tenantId)
        {
            throw new NotFoundException("Position", request.PositionId);
        }

        // --- HATA DÜZELTME: Artık takma adı (PersonnelEntity) kullanıyoruz. ---
        var newPersonnel = _mapper.Map<PersonnelEntity>(request);
        newPersonnel.TenantId = tenantId;

        var (result, userId) = await _identityService.CreateUserAsync(
            tenantId, 
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password,
            request.Roles);
            
        if (!result.Success)
        {
            throw new ValidationException(result.Message);
        }

        newPersonnel.UserId = userId;
        
        await _personnelRepository.AddAsync(newPersonnel);
        
        await _mediator.Publish(new PersonnelCreatedEvent(newPersonnel), cancellationToken);
        
        return _mapper.Map<PersonnelSummaryDto>(newPersonnel);
    }
}