using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class ProductsRepository : BaseRepository, IProductsRepository
{
    protected ProductsRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<Products>> GetProductsValuesAsync() =>
        await _db.Products.ToListAsync();

    public async Task<Products?> GetProductByIdAsync(int id) =>
        await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

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