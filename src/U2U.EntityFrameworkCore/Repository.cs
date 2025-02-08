namespace U2U.EntityFrameworkCore;

/// <summary>
/// This is an automatic implementation for IRepository.
/// </summary>
/// <typeparam name="T">The entity class' type.</typeparam>
/// <typeparam name="D">The DbContext to use.</typeparam>
public class Repository<T, D> : ReadonlyRepository<T, D>, IRepository<T>
  where T : class, IAggregateRoot
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

  private IEnumerable<IEntityInspector> GetEntityInspectors(EntityEntry entry)
  {
    IServiceProvider services = (DbContext as IInfrastructure<IServiceProvider>).Instance;
    Type serviceType = typeof(IEntityInspector<>).MakeGenericType(entry.Entity.GetType());
    IEnumerable<object?> inspectors = services.GetServices(serviceType: serviceType);
    return (IEnumerable<IEntityInspector> )inspectors;
  }

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

  private void Inspect(InspectorContext context)
  {
    IEnumerable<IEntityInspector> inspectors = GetEntityInspectors(context.Entry);
    foreach (IEntityInspector inspector in inspectors)
    {
      inspector.Inspect(context);
    }
  }

  public virtual async ValueTask SaveChangesAsync()
  {
    DateTime timestamp = DateTime.UtcNow;
    foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry? entry in DbContext.ChangeTracker.Entries())
    {
      Inspect(new InspectorContext(entry, timestamp));
      if (entry.Entity != null && entry.Entity is EntityBase)
      {
        await DispatchEvents((EntityBase)entry.Entity);
      }
    }
    await DbContext.SaveChangesAsync();
  }
}

