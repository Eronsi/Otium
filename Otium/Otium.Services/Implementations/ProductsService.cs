using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _repository;
    
    public ProductsService(IProductsRepository repository) =>
        _repository = repository;

    public async Task<BaseResponse<List<Products>>> GetProductsByCategoryAsync(string category)
    {
        var products = await _repository.GetProductsByCategoryAsync(category);
        if (products.Count == 0)
            return new BaseResponse<List<Products>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "No products found"
            };

        return new BaseResponse<List<Products>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = products
        };
    }

    public async Task<BaseResponse<Products?>> GetProductByNameAsync(string name)
    {
        var product = await _repository.GetProductByNameAsync(name);
        if (product is null)
            return new BaseResponse<Products?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Product not found"
            };

        return new BaseResponse<Products?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = product
        };
    }

    public async Task<BaseResponse<Products>> AddProductAsync(Products product)
    {
        var newProduct = await _repository.AddProductAsync(product);
        if (!newProduct.Equals(product))
            return new BaseResponse<Products>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Error adding product"
            };

        return new BaseResponse<Products>
        {
            StatusCode = HttpStatusCode.OK,
            Data = newProduct
        };
    }

    public async Task<BaseResponse<Products>> UpdateProductAsync(Products product)
    {
        var updatedProduct = await _repository.UpdateProductAsync(product);
        if (!updatedProduct.Equals(product))
            return new BaseResponse<Products>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Error updating product"
            };

        return new BaseResponse<Products>
        {
            StatusCode = HttpStatusCode.OK,
            Data = updatedProduct
        };
    }

    public async Task<BaseResponse<bool>> DeleteProductAsync(int id)
    {
        var deleted = await _repository.DeleteProductAsync(id);
        if (!deleted)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Error deleting product"
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