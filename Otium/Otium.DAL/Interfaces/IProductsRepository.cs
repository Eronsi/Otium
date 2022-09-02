using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface IProductsRepository : IBaseRepository
{
    Task<List<Products>> GetProductsByCategoryAsync(string category);
    Task<Products?> GetProductByNameAsync(string name);
    Task<Products> AddProductAsync(Products product);
    Task<Products> UpdateProductAsync(Products product);
    Task<bool> DeleteProductAsync(int id);
}