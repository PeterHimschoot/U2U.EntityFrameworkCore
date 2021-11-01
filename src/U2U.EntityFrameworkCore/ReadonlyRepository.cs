namespace U2U.EntityFrameworkCore;

/// <summary>
/// This is an automatic implementation for IReadonlyRepository.
/// </summary>
/// <typeparam name="T">The entity class' type.</typeparam>
/// <typeparam name="D">The DbContext to use.</typeparam>

public class ReadonlyRepository<T, D> : IReadonlyRepository<T>
  where T : class, IAggregateRoot
  where D : DbContext
{
  protected D DbContext { get; }

  public ReadonlyRepository(D dbContext)
    => DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

  protected virtual IQueryable<T> Includes(IQueryable<T> q)
    => q;

  protected internal IQueryable<T> BuildQueryable(ISpecification<T> specification)
    => specification.BuildQueryable(Includes(DbContext.Set<T>().AsQueryable()));

  public virtual async ValueTask<IEnumerable<T>> ListAsync(ISpecification<T> specification)
    => await BuildQueryable(specification).ToListAsync();

  public virtual async ValueTask<T?> SingleAsync(ISpecification<T> specification)
    => await BuildQueryable(specification).SingleOrDefaultAsync();

  protected virtual void Inspect(EntityEntry entry, DateTime timeStamp)
  { }
}

