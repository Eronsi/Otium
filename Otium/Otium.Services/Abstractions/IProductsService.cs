using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface IProductsService : IDisposable
{
    Task<BaseResponse<List<Products>>> GetProductsByCategoryAsync(string category);
    Task<BaseResponse<Products?>> GetProductByNameAsync(string name);
    Task<BaseResponse<Products>> AddProductAsync(Products product);
    Task<BaseResponse<Products>> UpdateProductAsync(Products product);
    Task<BaseResponse<bool>> DeleteProductAsync(int id);
}