using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface ICallbacksRepository : IBaseRepository
{
    Task<List<Callbacks>> GetAllAsync();
    Task<Callbacks?> GetByIdAsync(int id);
    Task<Callbacks> CreateAsync(Callbacks callback);
}