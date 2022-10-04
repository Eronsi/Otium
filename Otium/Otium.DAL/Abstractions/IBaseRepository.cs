using System.Linq.Expressions;

namespace Otium.Repositories.Abstractions;

public interface IBaseRepository<T> : IDisposable where T : class
{
    Task<List<T>> GetAllAsync(bool descending = false);
    Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> RemoveAsync(T entity, int id);
}