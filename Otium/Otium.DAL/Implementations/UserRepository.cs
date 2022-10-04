using Otium.Domain.Models;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext db) : base(db)
    {
    }
}