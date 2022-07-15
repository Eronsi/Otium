using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface INewsRepository : IBaseRepository
{
    Task<List<News>> GetNewsAsync();
    Task<News?> GetNewsByIdAsync(int id);
    Task<News> AddNewsAsync(News news);
    Task<News> UpdateNewsAsync(News news);
    Task<bool> DeleteNewsAsync(int id);
}