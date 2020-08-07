using System.Threading.Tasks;

namespace U2U.EntityFrameworkCore.Abstractions
{
  /// <summary>
  /// IRepository<typeparamref name="T"/> supports all actions of a IReadonlyRepository<typeparamref name="T"/>
  /// adding mutation methods to insert, update, and delete.
  /// </summary>
  /// <typeparam name="T">The entity class' type.</typeparam>
  public interface IRepository<T> : IReadonlyRepository<T>
    where T : class//, IEntity
  {
    /// <summary>
    /// Insert a new instance
    /// </summary>
    /// <param name="entity">A new instance to insert.</param>
    void Insert(T entity);

    /// <summary>
    /// Insert a new instance, asynchronously
    /// </summary>
    /// <param name="entity">A new instance to insert.</param>
    ValueTask InsertAsync(T entity);

    /// <summary>
    /// Delete an instance.
    /// </summary>
    /// <param name="entity">The instance that needs to disappear.</param>
    void Delete(T entity);

    /// <summary>
    /// Delete an instance, asynchronously.
    /// </summary>
    /// <param name="entity">The instance that needs to disappear.</param>
    ValueTask DeleteAsync(T entity);

    /// <summary>
    /// Update an instance.
    /// </summary>
    /// <param name="entity">Some instance to update.</param>
    void Update(T entity);

    /// <summary>
    /// Update an instance, asynchronously.
    /// </summary>
    /// <param name="entity">Some instance to update.</param>
    ValueTask UpdateAsync(T entity);

    ValueTask SaveChangesAsync();

  }
}
