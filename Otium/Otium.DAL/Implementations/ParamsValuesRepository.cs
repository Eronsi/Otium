using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class ParamsValuesRepository : BaseRepository, IParamsValuesRepository
{
    public ParamsValuesRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<ParamsValues>> GetParamsValuesAsync() =>
        await _db.ParamsValues.ToListAsync();

    public async Task<ParamsValues?> GetParamValueByIdAsync(int paramValueId) =>
        await _db.ParamsValues.FirstOrDefaultAsync(x => x.Id == paramValueId);

    public async Task<List<ParamsValues>> GetParamValuesAsync(int paramId) =>
        await _db.ParamsValues.Where(x => x.ParamId == paramId).ToListAsync();

    public async Task<ParamsValues> AddParamValueAsync(ParamsValues paramValue)
    {
        var entity = await _db.ParamsValues.AddAsync(paramValue);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<ParamsValues> UpdateParamValueAsync(ParamsValues paramValue)
    {
        var entity = _db.ParamsValues.Update(paramValue);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> DeleteParamValueAsync(int id)
    {
        var paramValue = await GetParamValueByIdAsync(id);
        if (paramValue is null)
            return await Task.FromResult(false);
        _db.ParamsValues.Remove(paramValue);
        await _db.SaveChangesAsync();
        return await Task.FromResult(true);
    }
}