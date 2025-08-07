using MediatR;

namespace EduHR.Application.Features.Plans.Commands;

/// <summary>
/// Represents the command to delete an existing Plan.
/// </summary>
public class DeletePlanCommand : IRequest
{
    public int Id { get; set; }

    public DeletePlanCommand(int id)
    {
        Id = id;
    }
}