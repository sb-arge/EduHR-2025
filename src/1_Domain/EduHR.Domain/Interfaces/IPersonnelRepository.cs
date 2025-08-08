using EduHR.Domain.Entities;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Personnel varlığı için veri erişim sözleşmesi.
/// </summary>
public interface IPersonnelRepository : IGenericRepository<Personnel>
{
    // Gelecekte, sadece Personel'e özgü olan sorgu metotları buraya eklenebilir.
    // Örneğin: Task<IEnumerable<Personnel>> GetPersonnelNearingRetirementAsync();
}