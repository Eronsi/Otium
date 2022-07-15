using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class CategoriesRepository : BaseRepository, ICategoriesRepository
{
    public CategoriesRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<Categories>> GetAllCategoriesAsync() =>
        await _db.Categories.ToListAsync();

    public async Task<Categories?> GetCategoryByIdAsync(int id) =>
        await _db.Categories.FirstOrDefaultAsync(categories => categories.Id == id);

    public async Task<Categories> CreateCategoryAsync(Categories category)
    {
        var entity = await _db.Categories.AddAsync(category);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }
}