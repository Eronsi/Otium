using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class ParamsService : IParamsService
{
    private readonly IParamsRepository _repository;

    public ParamsService(IParamsRepository paramsRepository) =>
        _repository = paramsRepository;
    
    public async Task<BaseResponse<List<Params>>> GetParamsAsync(int productId)
    {
        var paramsList = await _repository.GetParamsAsync(productId);
        if (paramsList.Count == 0)
            return new BaseResponse<List<Params>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "No params found"
            };

        return new BaseResponse<List<Params>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = paramsList
        };
    }

    public async Task<BaseResponse<Params?>> GetParamByIdAsync(int id)
    {
        var param = await _repository.GetParamByIdAsync(id);
        if (param is null)
            return new BaseResponse<Params?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Param not found"
            };

        return new BaseResponse<Params?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = param
        };
    }

    public async Task<BaseResponse<Params>> AddParamAsync(Params param)
    {
        var newParam = await _repository.AddParamAsync(param);
        if (!newParam.Equals(param))
            return new BaseResponse<Params>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Param not added"
            };

        return new BaseResponse<Params>
        {
            StatusCode = HttpStatusCode.OK,
            Data = newParam
        };
    }

    public async Task<BaseResponse<Params>> UpdateParamAsync(Params param)
    {
        var updatedParam = await _repository.UpdateParamAsync(param);
        if (!updatedParam.Equals(param))
            return new BaseResponse<Params>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Param not updated"
            };

        return new BaseResponse<Params>
        {
            StatusCode = HttpStatusCode.OK,
            Data = updatedParam
        };
    }

    public async Task<BaseResponse<bool>> DeleteParamAsync(int id)
    {
        var deleted = await _repository.DeleteParamAsync(id);
        if (!deleted)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "News not deleted"
            };

        return new BaseResponse<bool>
        {
            StatusCode = HttpStatusCode.OK,
            Data = deleted
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