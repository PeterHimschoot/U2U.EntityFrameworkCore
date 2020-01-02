﻿#nullable enable

using Microsoft.Extensions.Caching.Memory;
using System;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
  public static class MemoryCacheExtensions
  {
    public static R GetOrCreate<T, R>(this IMemoryCache cache, ISpecification<T> specification, Func<ISpecification<T>, R> getter)
      where T : class
    {
      var cs = specification as ICachedSpecification<T>;
      if (cs == null)
      {
        return getter(specification);
      }
      return cache.GetOrCreate(cs.Key, entry => getter(specification));
    }
  }
}
