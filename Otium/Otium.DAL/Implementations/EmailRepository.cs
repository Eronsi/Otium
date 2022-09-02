using Otium.Domain.Models;
using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class EmailRepository : BaseRepository, IEmailRepository
{
    public EmailRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<Guid> AddMailToDbAsync(Email email)
    {
        var entity = await _db.Emails.AddAsync(email);
        await _db.SaveChangesAsync();
        return entity.Entity.Id;
    }
}