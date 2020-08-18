using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
  /// <summary>
  /// Implementation for ISpecification<typeparamref name="T"/>
  /// </summary>
  /// <typeparam name="T">The entity class' type.</typeparam>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
  public class Specification<T> : ISpecification<T>, IEquatable<Specification<T>>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
   where T : class
  {
    public Specification(Expression<Func<T, bool>> criteria)
      : this(criteria, ImmutableList<Expression<Func<T, object>>>.Empty)
    { }

    public Specification(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> include)
      : this(criteria, new List<Expression<Func<T, object>>> { include }.ToImmutableList())
    { }

    public Specification(Expression<Func<T, bool>> criteria, IEnumerable<Expression<Func<T, object>>> includes)
    {
      Criteria = criteria;
      Includes = includes;
    }

    public Expression<Func<T, bool>> Criteria { get; }

    private Func<T, bool>? compiledCriteria = null;

    public bool Test(in T t)
    {
      this.compiledCriteria ??= Criteria.Compile();
      return this.compiledCriteria.Invoke(t);
    }

    public IEnumerable<Expression<Func<T, object>>> Includes { get; }

    public ISpecification<T> Include(IEnumerable<Expression<Func<T, object>>> includes)
    => includes == null ? this : new Specification<T>(Criteria, Includes.Union(includes));

    public ISpecification<T> Include(Expression<Func<T, object>> include)
    {
      if (include == null)
      {
        return this;
      }
      var includes = new List<Expression<Func<T, object>>>(Includes)
      {
        include
      };
      return Include(includes);
    }

    public IQueryable<T> BuildQueryable(IQueryable<T> q)
    => Includes.Aggregate(seed: q, func: (current, include) => current.Include(include))
      .Where(Criteria);

    public virtual bool Equals([AllowNull] Specification<T> other)
    {
      if (object.ReferenceEquals(this, other))
      {
        return true;
      }
      if (other == null)
      {
        return false;
      }
      return new ExpressionComparison(Criteria, other.Criteria).AreEqual;
    }

    public override bool Equals(object? obj)
    {
      if (object.ReferenceEquals(this, obj))
      {
        return true;
      }
      if (GetType() == obj?.GetType())
      {
        var spec = (Specification<T>)obj;
        return new ExpressionComparison(Criteria, spec.Criteria).AreEqual;
      }
      return false;
    }

    // No GetHashCode override, assuming you will never need a Specification as key in a dictionary
    // Prove me wrong :)
  }
}
