using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext Db;

    public BaseRepository(ApplicationDbContext db)
    {
        Db = db;
    }

    public async Task<List<T>> GetAllAsync(bool descending = false)
    {
        var data = Db.Set<T>();
        return descending ? await data.OrderByDescending(x => x).ToListAsync() : await data.ToListAsync();
    }

    public async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate) =>
        await Db.Set<T>().Where(predicate).ToListAsync();
    
    public async Task<T> AddAsync(T entity)
    {
        var createdEntity = await Db.Set<T>().AddAsync(entity);
        await Db.SaveChangesAsync();
        return createdEntity.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updatedEntity = Db.Set<T>().Update(entity);
        await Db.SaveChangesAsync();
        return updatedEntity.Entity;
    }
    public async Task<bool> RemoveAsync(T entity, int id)
    {
        try
        {
            Db.Set<T>().Remove(entity);
            await Db.CheckIdent(nameof(T), id);
            await Db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Db.Dispose();
            }
        }

        _disposed = true;
    }

    #endregion
}