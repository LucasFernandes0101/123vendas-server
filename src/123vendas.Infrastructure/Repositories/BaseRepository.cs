using _123vendas.Domain.Base.Interfaces;
using _123vendas.Domain.Base;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AspNetCore.IQueryable.Extensions.Filter;

namespace _123vendas.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
{
    protected readonly SqlDbContext _dbContext;

    public BaseRepository(SqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<T>> GetAsync(BaseFilter filter)
    {
        int count = (filter.Page - 1) * filter.MaxResults;

        IQueryable<T> query = _dbContext.Set<T>().AsQueryable().Filter(filter);
        return new PagedResult<T>(await query.CountAsync(), await query.Skip(count).Take(filter.MaxResults).Cast<T>().ToListAsync());
    }

    public async Task<PagedResult<T>> GetAsync(Expression<Func<T, bool>> criteria, int page = 1, int maxResults = 10)
    {
        page = page == 0 ? 1 : page;
        int count = (page - 1) * maxResults;

        IQueryable<T> query = _dbContext.Set<T>().AsQueryable().Where(criteria);
        return new PagedResult<T>(await query.CountAsync(), await query.Skip(count).Take(maxResults).ToListAsync());
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        if (entity.IsDeleted)
            throw new InvalidOperationException("The entity is already deleted.");

        entity.IsDeleted = true;

        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return entity;
    }
}