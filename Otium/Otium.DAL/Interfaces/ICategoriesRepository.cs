using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface ICategoriesRepository : IBaseRepository
{
    Task<List<Categories>> GetAllCategoriesAsync();
    Task<Categories?> GetCategoryByIdAsync(int id);
    Task<Categories> CreateCategoryAsync(Categories category);
}