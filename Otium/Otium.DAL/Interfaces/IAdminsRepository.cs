using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface IAdminsRepository : IBaseRepository
{
    Task<Admins?> GetByLoginAsync(string login);
}