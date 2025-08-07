using MediatR;

namespace EduHR.Application.Features.Roles.Commands;

public class DeleteRoleCommand : IRequest
{
    public int Id { get; set; }

    public DeleteRoleCommand(int id)
    {
        Id = id;
    }
}