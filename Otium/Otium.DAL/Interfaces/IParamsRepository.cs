using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface IParamsRepository : IBaseRepository
{
    Task<List<Params>> GetParamsAsync(int productId);
    Task<Params?> GetParamByIdAsync(int id);
    Task<Params> AddParamAsync(Params param);
    Task<Params> UpdateParamAsync(Params param);
    Task<bool> DeleteParamAsync(int id);
}