using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface IParamsValuesRepository : IBaseRepository
{
    Task<List<ParamsValues>> GetParamsValuesAsync();
    Task<ParamsValues?> GetParamValueByIdAsync(int paramValueId);
    Task<List<ParamsValues>> GetParamValuesAsync(int paramId);
    Task<ParamsValues> AddParamValueAsync(ParamsValues paramValue);
    Task<ParamsValues> UpdateParamValueAsync(ParamsValues paramValue);
    Task<bool> DeleteParamValueAsync(int id);
}