using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
  public static class SpecificationTestingExtensions
  {
    public static IQueryable<T> WithFakeData<T>(this ISpecification<T> spec, IQueryable<T> data)
      where T : class// , IEntity
      => spec.BuildQueryable(data);
  }
}
