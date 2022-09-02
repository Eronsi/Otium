using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface IEmailRepository : IBaseRepository
{
    Task<Guid> AddMailToDbAsync(Email email);
}