using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using User.Application.Interfaces;
using User.Domain.Common;

namespace User.Persistence.Interceptors;

internal sealed class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken()) {
        if ( eventData.Context is null ) {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        var identityService = eventData.Context.GetService<IIdentityService>();

        var modifiedEntries = eventData.Context.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Where(x => x.Entity is IBaseEntity);

        foreach ( var entry in modifiedEntries ) {
            if ( entry.Entity is IBaseEntity entity ) {
                entity.Update(identityService.GetUserId());
            } else {
                entry.State = EntityState.Unchanged;
            }
        }

        var addedEntries = eventData.Context.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added)
            .Where(x => x.Entity is IBaseEntity);

        foreach ( var entry in addedEntries ) {
            if ( entry.Entity is IBaseEntity entity ) {
                entity.Create(identityService.GetUserId());
            } else {
                entry.State = EntityState.Unchanged;
            }
        }

        var removedEntries = eventData.Context.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Deleted)
            .Where(x => x.Entity is ISoftDeleteBaseEntity);

        foreach ( var entry in removedEntries ) {
            if ( entry.Entity is ISoftDeleteBaseEntity entity ) {
                entity.Delete(identityService.GetUserId());
                entry.State = EntityState.Modified;
            } else {
                entry.State = EntityState.Unchanged;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
