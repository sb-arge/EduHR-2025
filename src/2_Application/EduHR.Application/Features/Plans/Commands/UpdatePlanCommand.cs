using MediatR;

namespace EduHR.Application.Features.Plans.Commands;

/// <summary>
/// Represents the command to update an existing Plan.
/// </summary>
public class UpdatePlanCommand : IRequest
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int UserLimit { get; set; }
    // İhtiyaç duyulursa, çevirileri güncellemek için bir DTO koleksiyonu da eklenebilir.
}