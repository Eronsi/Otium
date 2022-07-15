using Otium.Repositories.Interfaces;

namespace Otium.Repositories.Implementations;

public class BaseRepository : IBaseRepository
{
    protected readonly ApplicationDbContext _db;
    protected BaseRepository(ApplicationDbContext db) =>
        _db = db;

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        _disposed = true;
    }

    #endregion
}