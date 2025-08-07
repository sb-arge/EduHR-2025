using EduHR.Application.Exceptions;
using EduHR.Application.Features.Plans.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;

namespace EduHR.Application.Features.Plans.Handlers;

public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand>
{
    private readonly IPlanRepository _planRepository;
    private readonly ISubscriptionRepository _subscriptionRepository; // Bağımlılık eklendi

    public DeletePlanCommandHandler(IPlanRepository planRepository, ISubscriptionRepository subscriptionRepository)
    {
        _planRepository = planRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task Handle(DeletePlanCommand request, CancellationToken cancellationToken)
    {
        var planToDelete = await _planRepository.GetByIdAsync(request.Id);

        if (planToDelete is null)
        {
            throw new NotFoundException(nameof(Plan), request.Id);
        }

        // --- İŞ KURALI KONTROLÜ ---
        // Bu plana ait aktif bir abonelik olup olmadığını kontrol et.
        var hasActiveSubscriptions = await _subscriptionRepository.AnyActiveByPlanIdAsync(request.Id);
        if (hasActiveSubscriptions)
        {
            // Özel bir domain hatası fırlatılabilir.
            throw new EntityCannotBeDeletedException("Bu plan, aktif abonelikler tarafından kullanıldığı için silinemez.");
        }

        _planRepository.Delete(planToDelete);
    }
}