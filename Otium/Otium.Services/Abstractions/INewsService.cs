using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface INewsService : IDisposable
{
    Task<BaseResponse<List<News>>> GetNewsAsync();
    Task<BaseResponse<News?>> GetNewsByIdAsync(int id);
    Task<BaseResponse<News>> AddNewsAsync(News news);
    Task<BaseResponse<News>> UpdateNewsAsync(News news);
    Task<BaseResponse<bool>> DeleteNewsAsync(int id);
}