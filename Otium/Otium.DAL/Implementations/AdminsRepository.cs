using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class AdminsRepository : BaseRepository, IAdminsRepository
{
    public AdminsRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<Admins?> GetByLoginAsync(string login) =>
        await _db.Admins.FirstOrDefaultAsync(admin => admin.Login == login);
}