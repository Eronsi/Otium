using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class NewsService : INewsService
{
    private readonly INewsRepository _repository;

    public NewsService(INewsRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<List<News>>> GetNewsAsync()
    {
        var news = await _repository.GetNewsAsync();
        if (news.Count == 0)
            return new BaseResponse<List<News>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "No news found"
            };

        return new BaseResponse<List<News>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = news
        };
    }

    public async Task<BaseResponse<News?>> GetNewsByIdAsync(int id)
    {
        var news = await _repository.GetNewsByIdAsync(id);
        if (news is null)
            return new BaseResponse<News?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "News not found"
            };

        return new BaseResponse<News?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = news
        };
    }

    public async Task<BaseResponse<News>> AddNewsAsync(News news)
    {
        var addedNews = await _repository.AddNewsAsync(news);
        if (!addedNews.Equals(news))
            return new BaseResponse<News>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Description = "News not added"
            };

        return new BaseResponse<News>
        {
            StatusCode = HttpStatusCode.OK,
            Data = addedNews
        };
    }

    public async Task<BaseResponse<News>> UpdateNewsAsync(News news)
    {
        var updatedNews = await _repository.UpdateNewsAsync(news);
        if (!updatedNews.Equals(news))
            return new BaseResponse<News>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Description = "News not updated"
            };

        return new BaseResponse<News>
        {
            StatusCode = HttpStatusCode.OK,
            Data = updatedNews
        };
    }

    public async Task<BaseResponse<bool>> DeleteNewsAsync(int id)
    {
        var deleted = await _repository.DeleteNewsAsync(id);
        if (!deleted)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Description = "News not deleted"
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