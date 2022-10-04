using Otium.Domain.Models;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class NewsRepository : BaseRepository<News>, INewsRepository
{
    public NewsRepository(ApplicationDbContext db) : base(db)
    {
    }
}