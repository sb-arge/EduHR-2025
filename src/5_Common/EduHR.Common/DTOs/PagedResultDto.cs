// Fiziksel Yol: src/5_Common/EduHR.Common/DTOs/PlanDto.cs

namespace EduHR.Common.DTOs; // <-- Namespace'in sonunda .DTOs olması kritik.

public class PagedResultDto<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}