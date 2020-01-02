#nullable enable

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
  /// <summary>
  /// A Specification with extra caching information.
  /// </summary>
  /// <typeparam name="T">The entity class' type.</typeparam>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
  public class CachedSpecification<T>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    : Specification<T>
    , IEquatable<CachedSpecification<T>>
    , ICachedSpecification<T>
     where T : class//, IEntity
  {
    public CachedSpecification(Expression<Func<T, bool>> criteria, IEnumerable<Expression<Func<T, object>>> includes, TimeSpan cacheDuration, object? key = null)
    : base(criteria, includes)
    {
      CacheDuration = cacheDuration;
      Key = key ?? this;
    }

    public object Key { get; }

    public TimeSpan CacheDuration { get; set; }

    public bool Equals(CachedSpecification<T> other)
    {
      if (object.ReferenceEquals(this, other))
      {
        return true;
      }
      if (other == null)
      {
        return false;
      }
      return EqualityComparer<object>.Default.Equals(this.Key, other.Key)
        && (this.CacheDuration == other.CacheDuration)
        && base.Equals(other);
    }

    public override bool Equals(object? obj)
    {
      if (object.ReferenceEquals(this, obj))
      {
        return true;
      }

      if (this.GetType() == obj?.GetType())
      {
        CachedSpecification<T> other = (CachedSpecification<T>)obj;
        return EqualityComparer<object>.Default.Equals(this.Key, other.Key)
          && (this.CacheDuration == other.CacheDuration)
          && base.Equals(other);
      }
      return false;
    }
  }
}
