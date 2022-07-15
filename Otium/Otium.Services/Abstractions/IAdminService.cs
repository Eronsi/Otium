using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface IAdminService : IDisposable
{
    Task<BaseResponse<Admins>> GetByLoginAsync(string login);
}