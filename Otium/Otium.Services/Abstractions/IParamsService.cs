﻿using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface IParamsService : IDisposable
{
    Task<BaseResponse<List<Params>>> GetParamsAsync(int productId);
    Task<BaseResponse<Params>> AddParamAsync(Params param);
    Task<BaseResponse<Params>> UpdateParamAsync(Params param);
    Task<BaseResponse<bool>> DeleteParamAsync(int id);
}