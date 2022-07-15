using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class NewsRepository : BaseRepository, INewsRepository
{
    public NewsRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<News>> GetNewsAsync() =>
        await _db.News.ToListAsync();

    public async Task<News?> GetNewsByIdAsync(int id) =>
        await _db.News.FirstOrDefaultAsync(news => news.Id == id);

    public async Task<News> AddNewsAsync(News news)
    {
        var entity = _db.News.Add(news);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<News> UpdateNewsAsync(News news)
    {
        var entity = _db.News.Update(news);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> DeleteNewsAsync(int id)
    {
        var entity = _db.News.FirstOrDefault(news => news.Id == id);
        if (entity is null)
            return await Task.FromResult(false);
        
        _db.News.Remove(entity);
        await _db.SaveChangesAsync();
        return await Task.FromResult(true);
    }
}