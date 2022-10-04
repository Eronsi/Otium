using System.Net;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Abstractions;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class NewsService : INewsService
{
    private readonly INewsRepository _repository;

    public NewsService(INewsRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseResponse<List<News>>> GetNewsAsync()
    {
        var news = await _repository.GetAllAsync(true);
        if (news.Count == 0)
            return new BaseResponse<List<News>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "No news found"
            };

        return new BaseResponse<List<News>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = news
        };
    }

    public async Task<BaseResponse<News?>> GetNewsByIdAsync(int id)
    {
        var news = await _repository.FindByAsync(n => n.Id == id);
        if (news.Count == 0)
            return new BaseResponse<News?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "News not found"
            };

        return new BaseResponse<News?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = news.First()
        };
    }

    public async Task<BaseResponse<News>> AddNewsAsync(News news)
    {
        var addedNews = await _repository.AddAsync(news);
        if (!addedNews.Equals(news))
            return new BaseResponse<News>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "News not added"
            };
        
        var isImageProcessed = ProcessImage(news.Image!, news.Id, out var exception);
        if (!isImageProcessed)
            return new BaseResponse<News>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = exception
            };

        return new BaseResponse<News>
        {
            StatusCode = HttpStatusCode.OK,
            Data = addedNews
        };
    }

    public async Task<BaseResponse<News>> UpdateNewsAsync(News news)
    {
        var isImageProcessed = ProcessImage(news.Image!, news.Id, out var exception);
        if (!isImageProcessed)
            return new BaseResponse<News>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = exception
            };
        
        var updatedNews = await _repository.UpdateAsync(news);
        if (!updatedNews.Equals(news))
            return new BaseResponse<News>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "News not updated"
            };

        return new BaseResponse<News>
        {
            StatusCode = HttpStatusCode.OK,
            Data = updatedNews
        };
    }

    public async Task<BaseResponse<bool>> DeleteNewsAsync(int id)
    {
        var news = await _repository.FindByAsync(n => n.Id == id);
        if (news.Count == 0)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "News not found"
            };
        
        var deleted = await _repository.RemoveAsync(news.First(), id);
        if (!deleted)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "News not deleted"
            };
        
        var path = $"wwwroot/img/news/news{id}.jpg";
        if (File.Exists(path))
            File.Delete(path);

        return new BaseResponse<bool>
        {
            StatusCode = HttpStatusCode.OK,
            Data = deleted
        };
    }

    private static bool ProcessImage(IFormFile image, int id, out string exception)
    {
        exception = string.Empty;
        
        try
        {
            // Convert image to jpg
            using var imageMagick = new MagickImage(image.OpenReadStream());
            using var imageMemStream = new MemoryStream();
            imageMagick.Write(imageMemStream, MagickFormat.Jpg);
            
            // Write image to memory
            var path = $"wwwroot/img/news/news{id}.jpg";
            using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            imageMemStream.WriteTo(fileStream);
            fileStream.Close();

            // Optimize image
            var fileInfo = new FileInfo(path);
            var optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(fileInfo);
            fileInfo.Refresh();
            
            return true;
        }
        catch (Exception e)
        {
            exception = e.Message;
            return false;
        }
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