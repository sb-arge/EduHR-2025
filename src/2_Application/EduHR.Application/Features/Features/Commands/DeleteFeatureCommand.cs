using MediatR;

namespace EduHR.Application.Features.Features.Commands;

public class DeleteFeatureCommand : IRequest
{
    public int Id { get; set; }

    public DeleteFeatureCommand(int id)
    {
        Id = id;
    }
}