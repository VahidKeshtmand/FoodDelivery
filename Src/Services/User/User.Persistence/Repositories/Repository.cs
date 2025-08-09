using User.Domain.Common;
using User.Application.Interfaces;
using User.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace User.Persistence.Repositories;

internal class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> QueryNoTracking => _dbContext.Set<TEntity>().AsNoTracking();

    public IQueryable<TEntity> Query => _dbContext.Set<TEntity>().AsTracking();

    public DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    public int SaveChanges() {
        return _dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

internal class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity>
    where TEntity : class, IBaseEntity
{
    public Repository(AppDbContext dbContext) : base(dbContext) { }
}
