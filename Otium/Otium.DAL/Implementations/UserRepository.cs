using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username) =>
        await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
}