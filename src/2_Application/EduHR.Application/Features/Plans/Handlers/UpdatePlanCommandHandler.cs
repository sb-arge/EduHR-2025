using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Plans.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Plans.Handlers;

public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand>
{
    private readonly IPlanRepository _planRepository;
    private readonly IMapper _mapper;

    public UpdatePlanCommandHandler(IPlanRepository planRepository, IMapper mapper)
    {
        _planRepository = planRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
    {
        var planToUpdate = await _planRepository.GetByIdAsync(request.Id);

        if (planToUpdate is null)
        {
            throw new NotFoundException(nameof(Plan), request.Id);
        }

        // Gelen verileri mevcut varlık üzerine haritala/güncelle
        _mapper.Map(request, planToUpdate);
        
        _planRepository.Update(planToUpdate);
        
        // Değişikliklerin kaydedilmesi genellikle Unit of Work deseniyle
        // veya bir transaction behaviour ile yönetilir.
    }
}