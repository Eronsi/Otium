using Otium.Domain.Models;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class ProductsRepository : BaseRepository<Products>, IProductsRepository
{
    public ProductsRepository(ApplicationDbContext db) : base(db)
    {
    }
}