using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models;

namespace Otium.Repositories;

public class ApplicationDbContext : DbContext
{
#pragma warning disable CS8618
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
#pragma warning restore CS8618
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<Admins> Admins { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<Categories> Categories { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<News> News { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<Params> Params { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<ParamsValues> ParamsValues { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<Products> Products { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<Email> Emails { get; set; }
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DbSet<User> Users { get; set; }

    public async Task CheckIdent<T>(T entity, int reseedTo)
    {
        var tableName = entity?.GetType().Name ?? throw new ArgumentException("Entity is null");
        reseedTo = reseedTo == 1 ? 1 : reseedTo - 1;
        var sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, {reseedTo})";
        
        await Database.ExecuteSqlRawAsync(sql);
    }
}