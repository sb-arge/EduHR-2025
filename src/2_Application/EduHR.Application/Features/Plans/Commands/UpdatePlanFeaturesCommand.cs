using MediatR;

namespace EduHR.Application.Features.Plans.Commands;

/// <summary>
/// Bir Plan'ın sahip olduğu özellikleri güncellemek için komutu temsil eder.
/// </summary>
public class UpdatePlanFeaturesCommand : IRequest
{
    /// <summary>
    /// Özellikleri güncellenecek olan Plan'ın kimliği.
    /// </summary>
    public int PlanId { get; set; }

    /// <summary>
    /// Plan'a atanacak olan yeni Feature'ların kimlik listesi.
    /// Bu liste, planın mevcut özelliklerinin üzerine yazılacaktır.
    /// </summary>
    public ICollection<int> FeatureIds { get; set; } = new List<int>();
}