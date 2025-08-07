using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Organization.Commands;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Organization.Handlers;

/// <summary>
/// Handles the CreatePositionCommand.
/// </summary>
public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, PositionDto>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreatePositionCommandHandler(
        IPositionRepository positionRepository,
        IDepartmentRepository departmentRepository,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _positionRepository = positionRepository;
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PositionDto> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        // İlgili departmanın var olup olmadığını ve bu kiracıya ait olup olmadığını kontrol et.
        var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);
        if (department is null || department.TenantId != tenantId)
        {
            throw new NotFoundException(nameof(Department), request.DepartmentId);
        }

        var newPosition = _mapper.Map<Position>(request);
        newPosition.TenantId = tenantId;

        await _positionRepository.AddAsync(newPosition);
        
        // TODO: PositionCreatedEvent oluşturulup yayınlanabilir.

        return _mapper.Map<PositionDto>(newPosition);
    }
}