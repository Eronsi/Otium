using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface ICallbacksService : IDisposable
{
    Task<BaseResponse<List<Callbacks>>> GetAllAsync();
    Task<BaseResponse<Callbacks?>> GetByIdAsync(int id);
    Task<BaseResponse<Callbacks>> CreateAsync(Callbacks callback);
}