using Otium.Domain.Models;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class CategoriesRepository : BaseRepository<Categories>, ICategoriesRepository
{
    public CategoriesRepository(ApplicationDbContext db) : base(db)
    {
    }
}