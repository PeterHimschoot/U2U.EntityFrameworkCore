#nullable enable

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
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

    public virtual void Insert(T entity)
    {
      DbContext.Set<T>().Add(entity);
    }

    public virtual void Update(T entity)
    {
      DbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(T entity)
    {
      DbContext.Set<T>().Remove(entity);
    }

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
  }
}
