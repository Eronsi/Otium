using Otium.Domain.Models;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class ParamsValuesRepository : BaseRepository<ParamsValues>, IParamsValuesRepository
{
    public ParamsValuesRepository(ApplicationDbContext db) : base(db)
    {
    }
}