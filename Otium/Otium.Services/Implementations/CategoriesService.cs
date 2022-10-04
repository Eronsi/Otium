using System.Net;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Abstractions;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _repository;
    
    public CategoriesService(ICategoriesRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<List<Categories>>> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllAsync();
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
        var response = await _repository.FindByAsync(c => c.Id == id);
        if (response.Count == 0)
            return new BaseResponse<Categories?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Category not found"
            };

        return new BaseResponse<Categories?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = response.First()
        };
    }

    public async Task<BaseResponse<Categories?>> GetCategoryByNameAsync(string name)
    {
        var response = await _repository.FindByAsync(c => c.Name == name);
        if (response.Count == 0)
            return new BaseResponse<Categories?>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Category not found"
            };

        var category = response.First();
        return new BaseResponse<Categories?>
        {
            StatusCode = HttpStatusCode.OK,
            Data = category
        };
    }

    public async Task<BaseResponse<Categories>> AddCategoryAsync(Categories category)
    {
        category.Id = 0;
        var createdCategory = await _repository.AddAsync(category);
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

    public async Task<BaseResponse<Categories>> UpdateCategoryAsync(Categories category)
    {
        category.Id = 0;
        var updatedCategory = await _repository.UpdateAsync(category);
        if (!updatedCategory.Equals(category))
            return new BaseResponse<Categories>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "News not updated"
            };

        return new BaseResponse<Categories>
        {
            StatusCode = HttpStatusCode.OK,
            Data = updatedCategory
        };
    }

    public async Task<BaseResponse<bool>> DeleteCategoryAsync(int id)
    {
        var category = await _repository.FindByAsync(c => c.Id == id);
        if (category.Count == 0)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Category not found"
            };
        
        var deleted = await _repository.RemoveAsync(category.First(), id);
        if (!deleted)
            return new BaseResponse<bool>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Category not deleted"
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