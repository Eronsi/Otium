using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Abstractions;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class ParamsValuesService : IParamsValuesService
{
    private readonly IParamsValuesRepository _repository;

    public ParamsValuesService(IParamsValuesRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaseResponse<List<ParamsValues>>> GetParamValuesAsync(int paramId)
    {
        var paramValues = await _repository.FindByAsync(pv => pv.ParamId == paramId);
        if (paramValues.Count == 0)
            return new BaseResponse<List<ParamsValues>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Param values not found"
            };

        return new BaseResponse<List<ParamsValues>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = paramValues
        };
    }

    public async Task<BaseResponse<ParamsValues>> AddParamValueAsync(ParamsValues paramValue)
    {
        var newParamValue = await _repository.AddAsync(paramValue);
        if (!newParamValue.Equals(paramValue))
            return new BaseResponse<ParamsValues>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Error adding param value"
            };

        return new BaseResponse<ParamsValues>
        {
            StatusCode = HttpStatusCode.OK,
            Data = newParamValue
        };
    }

    public async Task<BaseResponse<ParamsValues>> UpdateParamValueAsync(ParamsValues paramValue)
    {
        var updatedParamValue = await _repository.UpdateAsync(paramValue);
        if (!updatedParamValue.Equals(paramValue))
            return new BaseResponse<ParamsValues>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Error updating param value"
            };

        return new BaseResponse<ParamsValues>
        {
            StatusCode = HttpStatusCode.OK,
            Data = updatedParamValue
        };
    }

    public async Task<BaseResponse<bool>> DeleteParamValueAsync(int id)
    {
        var response = await _repository.FindByAsync(pv => pv.Id == id);
        if (response.Count == 0)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Param value not found"
            };
        
        var deleted = await _repository.RemoveAsync(response.First(), id);
        if (!deleted)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Error deleting param value"
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