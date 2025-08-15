using Microsoft.EntityFrameworkCore;
using Users.Domain.Common;

namespace Users.Application.Interfaces;

public interface IRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
{
    DbSet<TEntity> DbSet { get; }
    IQueryable<TEntity> Query { get; }
    IQueryable<TEntity> QueryNoTracking { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IBaseEntity { }
