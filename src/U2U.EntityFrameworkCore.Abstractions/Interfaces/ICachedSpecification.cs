using System;

namespace U2U.EntityFrameworkCore.Abstractions
{
  /// <summary>
  /// Enhances an ISpecification with caching information.
  /// </summary>
  public interface ICachedSpecification<T> : ISpecification<T>
    where T : class//, IEntity
  {
    /// <summary>
    /// The key used in cache lookup.
    /// </summary>
    object Key { get; }

    /// <summary>
    /// The cache duration, obviously...
    /// </summary>
    TimeSpan CacheDuration { get; }
  }
}
