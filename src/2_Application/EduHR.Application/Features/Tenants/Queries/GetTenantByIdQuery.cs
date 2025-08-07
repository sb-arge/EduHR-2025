using AutoMapper;
using EduHR.Application.Features.Tenants.Queries;
using EduHR.Common.DTOs;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Tenants.Handlers;

public class GetAllTenantsQueryHandler : IRequestHandler<GetAllTenantsQuery, PagedResultDto<TenantDto>>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IMapper _mapper;

    public GetAllTenantsQueryHandler(ITenantRepository tenantRepository, IMapper mapper)
    {
        _tenantRepository = tenantRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<TenantDto>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
    {
        var tenants = await _tenantRepository.GetAllAsync(); // Gerçek uygulamada sayfalama yapılacak
        var tenantDtos = _mapper.Map<List<TenantDto>>(tenants);

        return new PagedResultDto<TenantDto>
        {
            Items = tenantDtos,
            TotalCount = tenantDtos.Count,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}