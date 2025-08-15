using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Domain.Common;
using Users.Persistence.DbContexts;

namespace Users.Persistence.Repositories;

internal class Repository<TEntity, TKey>(AppDbContext dbContext) : IRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>
{
    public IQueryable<TEntity> QueryNoTracking => dbContext.Set<TEntity>().AsNoTracking();

    public IQueryable<TEntity> Query => dbContext.Set<TEntity>().AsTracking();

    public DbSet<TEntity> DbSet => dbContext.Set<TEntity>();

    public int SaveChanges() {
        return dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}

internal class Repository<TEntity>(AppDbContext dbContext) : Repository<TEntity, int>(dbContext), IRepository<TEntity>
    where TEntity : class, IBaseEntity;
