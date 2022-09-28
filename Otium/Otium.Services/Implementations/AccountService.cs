using System.Net;
using System.Security.Claims;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Domain.ViewModels;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;
using Otium.Services.Utils;

namespace Otium.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository _repository;

    public AccountService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaseResponse<ClaimsIdentity>> LoginAsync(LoginViewModel model)
    {
        var user = await _repository.GetByUsernameAsync(model.Username);
        if (user is null || 
            !await PasswordService.VerifyPassword(model.Password,
                new PasswordService.PasswordHashRecord(user.PasswordSalt, user.PasswordHash)))
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Incorrect username or password"
            };

        var result = Authenticate(user);
        return new BaseResponse<ClaimsIdentity>
        {
            StatusCode = HttpStatusCode.OK,
            Data = result
        };
    }

    private static ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Username),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
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