using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface IEmailService
{
    Task<BaseResponse<string>> SendEmailAsync(Email request);
    Task<BaseResponse<List<Email>>> GetLastEmailsByIpAsync(string ip, int minutes);
}