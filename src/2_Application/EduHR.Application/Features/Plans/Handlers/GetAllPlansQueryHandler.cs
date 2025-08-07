using AutoMapper;
using EduHR.Application.Features.Plans.Queries;
using EduHR.Common.DTOs; // <-- DÜZELTİLDİ: Artık Common projesinden geliyor.
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Plans.Handlers;

/// <summary>
/// Handles the GetAllPlansQuery.
/// </summary>
public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, PagedResultDto<PlanDto>>
{
    private readonly IPlanRepository _planRepository;
    private readonly IMapper _mapper;

    public GetAllPlansQueryHandler(IPlanRepository planRepository, IMapper mapper)
    {
        _planRepository = planRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<PlanDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
    {
        // Bu kısım gelecekte daha karmaşık hale getirilebilir (sayfalama, sıralama vb.)
        var plans = await _planRepository.GetAllAsync();
        var planDtos = _mapper.Map<List<PlanDto>>(plans);

        var pagedResult = new PagedResultDto<PlanDto>
        {
            Items = planDtos,
            TotalCount = planDtos.Count, // Gerçek bir uygulamada bu, veritabanından ayrı bir sorgu ile alınır.
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
        
        return pagedResult;
    }
}