using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface IProductsRepository : IBaseRepository
{
    Task<List<Products>> GetProductsValuesAsync();
    Task<Products?> GetProductByIdAsync(int id);
    Task<Products> AddProductAsync(Products product);
    Task<Products> UpdateProductAsync(Products product);
    Task<bool> DeleteProductAsync(int id);
}