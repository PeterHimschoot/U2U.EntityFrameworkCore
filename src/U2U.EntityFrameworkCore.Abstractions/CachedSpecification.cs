namespace U2U.EntityFrameworkCore;

/// <summary>
/// A Specification with extra caching information.
/// </summary>
/// <typeparam name="T">The entity class' type.</typeparam>
public class CachedSpecification<T, K>
    : Specification<T>
  , IEquatable<CachedSpecification<T, K>>
  where T : class
{
  public CachedSpecification(Expression<Func<T, bool>> criteria, IEnumerable<Expression<Func<T, object>>> includes, TimeSpan cacheDuration, K key)
  : base(criteria, includes)
  {
    CacheDuration = cacheDuration;
    Key = key;
  }

  public CachedSpecification(Expression<Func<T, bool>> criteria, TimeSpan cacheDuration, K key)
  : base(criteria)
  {
    CacheDuration = cacheDuration;
    Key = key;
  }

  public K Key { get; }

  public TimeSpan CacheDuration { get; set; }

  public bool Equals(CachedSpecification<T, K>? other)
  {
    if (object.ReferenceEquals(this, other))
    {
      return true;
    }
    if (other is null)
    {
      return false;
    }
    return EqualityComparer<K>.Default.Equals(this.Key, other.Key)
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
      CachedSpecification<T, K> other = (CachedSpecification<T, K>)obj;
      return this.Equals(other);
    }
    return false;
  }

  public override int GetHashCode() 
    => HashCode.Combine(this.Criteria, this.CacheDuration, this.Key);
}

