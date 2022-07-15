using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class AdminsService : IAdminService
{
    private readonly IAdminsRepository _repository;
    
    public AdminsService(IAdminsRepository adminRepository) =>
        _repository = adminRepository;

    public async Task<BaseResponse<Admins>> GetByLoginAsync(string login)
    {
        var admin = await _repository.GetByLoginAsync(login);
        if (admin is null)
            return new BaseResponse<Admins>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Admin not found"
            };

        return new BaseResponse<Admins>
        {
            StatusCode = HttpStatusCode.OK,
            Data = admin
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