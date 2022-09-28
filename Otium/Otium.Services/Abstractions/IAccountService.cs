using System.Security.Claims;
using Otium.Domain.Response;
using Otium.Domain.ViewModels;

namespace Otium.Services.Abstractions;

public interface IAccountService : IDisposable
{
    Task<BaseResponse<ClaimsIdentity>> LoginAsync(LoginViewModel model);
}