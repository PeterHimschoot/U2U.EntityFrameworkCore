#nullable enable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
  /// <summary>
  /// This is an automatic implementation for IReadonlyRepository.
  /// </summary>
  /// <typeparam name="T">The entity class' type.</typeparam>
  /// <typeparam name="D">The DbContext to use.</typeparam>

  public class ReadonlyRepository<T, D> : IReadonlyRepository<T>
    where T : class//, IEntity
    where D : DbContext
  {
    protected D DbContext { get; }

    public ReadonlyRepository(D dbContext)
      => DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    protected virtual IQueryable<T> Includes(IQueryable<T> q)
      => q;

    protected internal IQueryable<T> BuildQueryable(ISpecification<T> specification)
      => specification
      .BuildQueryable(Includes(DbContext.Set<T>().AsQueryable()));

    public virtual IEnumerable<T> List(ISpecification<T> specification)
      => BuildQueryable(specification).AsEnumerable();

    public virtual T? Single(ISpecification<T> specification)
      => BuildQueryable(specification).SingleOrDefault();

    public virtual async ValueTask<IEnumerable<T>> ListAsync(ISpecification<T> specification)
      => await BuildQueryable(specification).ToListAsync();

    public virtual async ValueTask<T?> SingleAsync(ISpecification<T> specification)
      => await BuildQueryable(specification).SingleOrDefaultAsync();

    protected virtual void Inspect(EntityEntry entry, DateTime timeStamp)
    { }
  }
}
