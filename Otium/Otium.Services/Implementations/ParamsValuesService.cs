using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class ParamsValuesService : IParamsValuesService
{
    private readonly IParamsValuesRepository _repository;
    
    public ParamsValuesService(IParamsValuesRepository repository) =>
        _repository = repository;

    public async Task<BaseResponse<List<ParamsValues>>> GetParamsValuesAsync()
    {
        var paramsValues = await _repository.GetParamsValuesAsync();
        return new BaseResponse<List<ParamsValues>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = paramsValues
        };
    }

    public async Task<BaseResponse<ParamsValues?>> GetParamValueByIdAsync(int paramValueId)
    {
        var paramValue = await _repository.GetParamValueByIdAsync(paramValueId);
        if (paramValue is null)
            return new BaseResponse<ParamsValues?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Param value not found"
            };

        return new BaseResponse<ParamsValues?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = paramValue
        };
    }

    public async Task<BaseResponse<List<ParamsValues>>> GetParamValuesAsync(int paramId)
    {
        var paramValues = await _repository.GetParamValuesAsync(paramId);
        if (paramValues.Count == 0)
            return new BaseResponse<List<ParamsValues>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Param values not found"
            };

        return new BaseResponse<List<ParamsValues>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = paramValues
        };
    }

    public async Task<BaseResponse<ParamsValues>> AddParamValueAsync(ParamsValues paramValue)
    {
        var newParamValue = await _repository.AddParamValueAsync(paramValue);
        if (!newParamValue.Equals(paramValue))
            return new BaseResponse<ParamsValues>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Description = "Error adding param value"
            };

        return new BaseResponse<ParamsValues>
        {
            StatusCode = HttpStatusCode.OK,
            Data = newParamValue
        };
    }

    public async Task<BaseResponse<ParamsValues>> UpdateParamValueAsync(ParamsValues paramValue)
    {
        var updatedParamValue = await _repository.UpdateParamValueAsync(paramValue);
        if (!updatedParamValue.Equals(paramValue))
            return new BaseResponse<ParamsValues>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Description = "Error updating param value"
            };

        return new BaseResponse<ParamsValues>
        {
            StatusCode = HttpStatusCode.OK,
            Data = updatedParamValue
        };
    }

    public async Task<BaseResponse<bool>> DeleteParamValueAsync(int id)
    {
        var deleted = await _repository.DeleteParamValueAsync(id);
        if (!deleted)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Description = "Error deleting param value"
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