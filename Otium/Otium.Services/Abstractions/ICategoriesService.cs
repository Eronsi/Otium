using Otium.Domain.Models;
using Otium.Domain.Response;

namespace Otium.Services.Abstractions;

public interface ICategoriesService : IDisposable
{
    Task<BaseResponse<List<Categories>>> GetAllCategoriesAsync();
    Task<BaseResponse<Categories?>> GetCategoryByIdAsync(int id);
    Task<BaseResponse<Categories>> CreateCategoryAsync(Categories category);
}