using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface IParamsValuesService : IDisposable
{
    Task<BaseResponse<List<ParamsValues>>> GetParamsValuesAsync();
    Task<BaseResponse<ParamsValues?>> GetParamValueByIdAsync(int paramValueId);
    Task<BaseResponse<List<ParamsValues>>> GetParamValuesAsync(int paramId);
    Task<BaseResponse<ParamsValues>> AddParamValueAsync(ParamsValues paramValue);
    Task<BaseResponse<ParamsValues>> UpdateParamValueAsync(ParamsValues paramValue);
    Task<BaseResponse<bool>> DeleteParamValueAsync(int id);
}