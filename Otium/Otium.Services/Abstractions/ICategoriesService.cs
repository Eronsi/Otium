using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface ICategoriesService : IDisposable
{
    Task<BaseResponse<List<Categories>>> GetAllCategoriesAsync();
    Task<BaseResponse<Categories?>> GetCategoryByIdAsync(int id);
    Task<BaseResponse<Categories?>> GetCategoryByNameAsync(string name);
    Task<BaseResponse<Categories>> AddCategoryAsync(Categories category);
    Task<BaseResponse<Categories>> UpdateCategoryAsync(Categories category);
    Task<BaseResponse<bool>> DeleteCategoryAsync(int id);
}