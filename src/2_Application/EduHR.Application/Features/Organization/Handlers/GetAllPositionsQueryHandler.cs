using AutoMapper;
using EduHR.Application.Features.Organization.Queries;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Organization.Handlers;

/// <summary>
/// Handles the GetAllPositionsQuery.
/// </summary>
public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, IEnumerable<PositionDto>>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAllPositionsQueryHandler(
        IPositionRepository positionRepository,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<PositionDto>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        // DÜZELTME: Artık kiracıya özel repository metodunu kullanıyoruz.
        var positions = await _positionRepository.GetAllByTenantIdAsync(tenantId);

        // İsteğe bağlı departman filtresini uygula
        if (request.DepartmentId.HasValue)
        {
            positions = positions.Where(p => p.DepartmentId == request.DepartmentId.Value);
        }
        
        return _mapper.Map<IEnumerable<PositionDto>>(positions);
    }
}