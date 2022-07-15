using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class ParamsRepository : BaseRepository, IParamsRepository
{
    protected ParamsRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<Params>> GetParamsAsync() =>
        await _db.Params.ToListAsync();

    public async Task<Params?> GetParamByIdAsync(int id) =>
        await _db.Params.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Params> AddParamAsync(Params param)
    {
        var entity = _db.Params.Add(param);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Params> UpdateParamAsync(Params param)
    {
        var entity = _db.Params.Update(param);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> DeleteParamAsync(int id)
    {
        var param = _db.Params.FirstOrDefault(x => x.Id == id);
        if (param is null)
            return await Task.FromResult(false);
        
        _db.Params.Remove(param);
        await _db.SaveChangesAsync();
        return await Task.FromResult(true);
    }
}