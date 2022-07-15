using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class CallbacksRepository : BaseRepository, ICallbacksRepository
{
    public CallbacksRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<Callbacks>> GetAllAsync() =>
        await _db.Callbacks.ToListAsync();

    public async Task<Callbacks?> GetByIdAsync(int id) =>
        await _db.Callbacks.FirstOrDefaultAsync(callback => callback.Id == id);

    public async Task<Callbacks> CreateAsync(Callbacks callback)
    {
        var entity = await _db.Callbacks.AddAsync(callback);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }
}