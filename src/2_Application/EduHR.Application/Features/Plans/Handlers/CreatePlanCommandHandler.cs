using AutoMapper;
using EduHR.Application.Features.Plans.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Exceptions;
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Plans.Handlers;

/// <summary>
/// Handles the creation of a new Plan.
/// </summary>
public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, CreatePlanCommandResponse>
{
    private readonly IPlanRepository _planRepository;
    private readonly IMapper _mapper;

    public CreatePlanCommandHandler(IPlanRepository planRepository, IMapper mapper)
    {
        _planRepository = planRepository;
        _mapper = mapper;
    }

    public async Task<CreatePlanCommandResponse> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        // PlanCode'un benzersiz olduğunu kontrol et
        var existingPlan = await _planRepository.GetByCodeAsync(request.PlanCode);
        if (existingPlan is not null)
        {
            throw DuplicateEntityException.ForEntity("Plan", "Plan Code", request.PlanCode);
        }

        var newPlan = _mapper.Map<Plan>(request);

        await _planRepository.AddAsync(newPlan);
        
        return _mapper.Map<CreatePlanCommandResponse>(newPlan);
    }
}