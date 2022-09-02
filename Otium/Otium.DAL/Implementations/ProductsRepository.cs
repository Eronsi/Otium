using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class ProductsRepository : BaseRepository, IProductsRepository
{
    public ProductsRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<Products>> GetProductsByCategoryAsync(string category) =>
        await _db.Products.Where(p => p.CategoryName == category).ToListAsync();

    public async Task<Products?> GetProductByNameAsync(string name) =>
        await _db.Products.FirstOrDefaultAsync(p => p.Name == name);

    public async Task<Products> AddProductAsync(Products product)
    {
        var entity = _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Products> UpdateProductAsync(Products product)
    {
        var entity = _db.Products.Update(product);
        await _db.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await  _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
            return await Task.FromResult(false);
        
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return await Task.FromResult(false);
    }
}