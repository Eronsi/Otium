using Otium.Domain.Models;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class ParamsRepository : BaseRepository<Params>, IParamsRepository
{
    public ParamsRepository(ApplicationDbContext db) : base(db)
    {
    }
}