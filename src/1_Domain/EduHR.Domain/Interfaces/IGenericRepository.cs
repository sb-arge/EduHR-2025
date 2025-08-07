using EduHR.Domain.Common;
using System.Linq.Expressions;

namespace EduHR.Domain.Interfaces;

/// <summary>
/// Tüm repository'ler için temel CRUD operasyonlarını tanımlayan jenerik arayüz.
/// </summary>
/// <typeparam name="T">BaseEntity'den miras alan bir varlık tipi.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}