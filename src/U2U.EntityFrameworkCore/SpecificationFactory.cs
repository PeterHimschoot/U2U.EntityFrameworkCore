#nullable enable

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.EntityFrameworkCore.Abstractions.Interfaces;

namespace U2U.EntityFrameworkCore
{
  public class SpecificationFactory<T>
    : ISpecificationFactory
    where T : class// , IEntity
  {
    // Flyweights
    private readonly ISpecification<T> none = new Specification<T>(t => false);
    private readonly ISpecification<T> all = new Specification<T>(t => true);

    public ISpecification<T> None() => none;

    public ISpecification<T> All() => all;

    public ISpecification<T> Where(Expression<Func<T, bool>> condition)
      => new Specification<T>(condition);

    public virtual bool ForType(Type t) 
      => typeof(T) == t;
  }

  public static class SpecificationFactoryExtensions
  {
    public static ISpecification<T> WithId<T>(this SpecificationFactory<T> _, int id)
      where T : EntityBase
      => new EntitySpecification<T>(id);

  }
}
