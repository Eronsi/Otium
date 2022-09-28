using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _repository;
    
    public CategoriesService(ICategoriesRepository categoriesRepository) =>
        _repository = categoriesRepository;
    
    public async Task<BaseResponse<List<Categories>>> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllCategoriesAsync();
        if (categories.Count == 0)
            return new BaseResponse<List<Categories>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "No categories found"
            };

        return new BaseResponse<List<Categories>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = categories
        };
    }

    public async Task<BaseResponse<Categories?>> GetCategoryByIdAsync(int id)
    {
        var category = await _repository.GetCategoryByIdAsync(id);
        if (category is null)
            return new BaseResponse<Categories?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Category not found"
            };

        return new BaseResponse<Categories?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = category
        };
    }

    public async Task<BaseResponse<Categories?>> GetCategoryByNameAsync(string name)
    {
        var category = await _repository.GetCategoryByNameAsync(name);
        if (category is null)
            return new BaseResponse<Categories?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Category not found"
            };

        return new BaseResponse<Categories?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = category
        };
    }

    public async Task<BaseResponse<Categories>> CreateCategoryAsync(Categories category)
    {
        var createdCategory = await _repository.CreateCategoryAsync(category);
        if (!createdCategory.Equals(category))
            return new BaseResponse<Categories>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Category not created"
            };

        return new BaseResponse<Categories>
        {
            StatusCode = HttpStatusCode.OK,
            Data = createdCategory
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