using System.Collections.Generic;
using System.Threading.Tasks;

namespace U2U.EntityFrameworkCore.Abstractions
{
  /// <summary>
  /// IReadonlyRepository<typeparamref name="T"/> allows you to retrieve entities
  /// - by specification
  /// You can also choose between a list of entities, or a single instance.
  /// </summary>
  /// <typeparam name="T">The entity class' type.</typeparam>
  public interface IReadonlyRepository<T> where T : class
  {
    /// <summary>
    /// Retrieve the list of all entities by specification, asynchronously.
    /// </summary>
    /// <param name="specification">The specification containing the filter and includes.</param>
    /// <returns>A IEnumerable<typeparamref name="T"/> of entities.</returns>
    ValueTask<IEnumerable<T>> ListAsync(ISpecification<T> specification);

    /// <summary>
    /// Retrieve a single instance by specification, asynchronously.
    /// </summary>
    /// <param name="specification">The specification containing the filter and includes.</param>
    /// <returns>The entity matching the specification, or null if none found.</returns>
    ValueTask<T?> SingleAsync(ISpecification<T> specification);
  }
}
