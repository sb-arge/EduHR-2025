using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Personnel.Commands;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using EduHR.Domain.Events;
using EduHR.Domain.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

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

        // İlgili pozisyonun var olup olmadığını ve bu kiracıya ait olup olmadığını kontrol et.
        var position = await _positionRepository.GetByIdAsync(request.PositionId);
        if (position is null || position.TenantId != tenantId)
        {
            throw new NotFoundException(nameof(Position), request.PositionId);
        }

        // Yeni personel varlığını oluştur.
        var newPersonnel = _mapper.Map<Personnel>(request);
        newPersonnel.TenantId = tenantId;

        // Personel için bir kullanıcı hesabı oluştur.
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

        // Oluşturulan kullanıcı ID'sini personel kaydına bağla.
        newPersonnel.UserId = userId;
        
        // Personeli veritabanına ekle.
        await _personnelRepository.AddAsync(newPersonnel);
        
        // Olayı yayınla.
        await _mediator.Publish(new PersonnelCreatedEvent(newPersonnel), cancellationToken);
        
        // Sonucu DTO olarak geri dön.
        return _mapper.Map<PersonnelSummaryDto>(newPersonnel);
    }
}