using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class CallbacksService : ICallbacksService
{
    private readonly ICallbacksRepository _repository;

    public CallbacksService(ICallbacksRepository callbacksRepository) =>
        _repository = callbacksRepository;
    
    public async Task<BaseResponse<List<Callbacks>>> GetAllAsync()
    {
        var callbacks = await _repository.GetAllAsync();
        if (callbacks.Count == 0)
            return new BaseResponse<List<Callbacks>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "No callbacks found"
            };

        return new BaseResponse<List<Callbacks>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = callbacks
        };
    }

    public async Task<BaseResponse<Callbacks?>> GetByIdAsync(int id)
    {
        var callback = await _repository.GetByIdAsync(id);
        if (callback is null)
            return new BaseResponse<Callbacks?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Callback not found"
            };

        return new BaseResponse<Callbacks?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = callback
        };
    }

    public async Task<BaseResponse<Callbacks>> CreateAsync(Callbacks callback)
    {
        var result = await _repository.CreateAsync(callback);
        if (!callback.Equals(result))
            return new BaseResponse<Callbacks>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Description = "Callback not created"
            };

        return new BaseResponse<Callbacks>
        {
            StatusCode = HttpStatusCode.OK,
            Data = result
        };
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
                _repository.Dispose();
            }
        }

        _disposed = true;
    }

    #endregion
}