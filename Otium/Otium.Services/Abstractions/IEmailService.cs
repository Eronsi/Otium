using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface IEmailService : IDisposable
{
    Task<BaseResponse<Guid>> SendEmailAsync(Email request);
}