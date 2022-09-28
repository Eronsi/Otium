using Otium.Domain.Models;

namespace Otium.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository
{
    Task<User?> GetByUsernameAsync(string username);
}