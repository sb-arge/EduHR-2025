using MediatR;

namespace EduHR.Application.Features.Organization.Commands;

public class DeletePositionCommand : IRequest
{
    public int Id { get; set; }

    public DeletePositionCommand(int id)
    {
        Id = id;
    }
}