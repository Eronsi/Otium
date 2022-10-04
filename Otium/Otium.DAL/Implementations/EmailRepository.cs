using Otium.Domain.Models;
using Otium.Repositories.Abstractions;

namespace Otium.Repositories.Implementations;

public class EmailRepository : BaseRepository<Email>, IEmailRepository
{
    public EmailRepository(ApplicationDbContext db) : base(db)
    {
    }
}