using System.Linq.Expressions;

namespace _123vendas.Domain.Base.Interfaces;

public interface IBaseRepository<T>
{
    Task<PagedResult<T>> GetAsync(int page = 1, int maxResults = 10, Expression<Func<T, bool>>? criteria = default);
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}