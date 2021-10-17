namespace U2U.EntityFrameworkCore;

/// <summary>
/// This is an automatic implementation for IRepository.
/// </summary>
/// <typeparam name="T">The entity class' type.</typeparam>
/// <typeparam name="D">The DbContext to use.</typeparam>
public class Repository<T, D> : ReadonlyRepository<T, D>, IRepository<T>
  where T : class//, IEntity
  where D : DbContext
{
  public Repository(D dbContext) : base(dbContext) { }

  public virtual ValueTask InsertAsync(T entity)
  {
    DbContext.Set<T>().Add(entity);
    return new ValueTask();
  }

  public virtual ValueTask DeleteAsync(T entity)
  {
    DbContext.Set<T>().Remove(entity);
    return new ValueTask();
  }

  public virtual ValueTask UpdateAsync(T entity)
  {
    DbContext.Entry(entity).State = EntityState.Modified;
    return new ValueTask();
  }

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0101:Array allocation for params parameter", Justification = "<Pending>")]
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "<Pending>")]
  private async ValueTask DispatchEvents(EntityBase entity)
  {
    IServiceProvider services = (DbContext as IInfrastructure<IServiceProvider>).Instance;
    if (entity.domainEvents != null)
    {
      foreach (IDomainEvent? @event in entity.domainEvents)
      {
        Type serviceType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
        IEnumerable<object?> domainEventHandlers = services.GetServices(serviceType: serviceType);
        foreach (IDomainEventHandler? handler in domainEventHandlers)
        {
          if (handler is not null)
          {
            await handler.Handle(@event);
          }
        }
      }
    }
  }

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "<Pending>")]
  public virtual async ValueTask SaveChangesAsync()
  {
    DateTime timestamp = DateTime.UtcNow;
    foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry? entry in DbContext.ChangeTracker.Entries())
    {
      Inspect(entry, timestamp);
      if (entry.Entity != null && entry.Entity is EntityBase)
      {
        await DispatchEvents((EntityBase)entry.Entity);
      }
    }
    await DbContext.SaveChangesAsync();
  }
}

