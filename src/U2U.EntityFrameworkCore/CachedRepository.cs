#nullable enable

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
  /// <summary>
  /// This is an automatic implementation for IReadonlyRepository, with caching.
  /// It uses the ReadonlyRepository to retrieve actual data, and stores it in the cache.
  /// </summary>
  /// <typeparam name="T">The entity class' type.</typeparam>
  /// <typeparam name="D">The DbContext to use.</typeparam>
  public class CachedRepository<T, D> : IReadonlyRepository<T>
    where T : class
    where D : DbContext
  {
    protected ReadonlyRepository<T, D> innerRepo;
    protected IMemoryCache cache;

    public CachedRepository(D dbContext, IMemoryCache cache)
    {
      this.innerRepo = new ReadonlyRepository<T, D>(dbContext);
      this.cache = cache;
    }

    //public IEnumerable<T> List(ISpecification<T> specification)
    //  => this.cache.GetOrCreate(specification, this.innerRepo.List);

    //public T? Single(ISpecification<T> specification)
    //  => this.cache.GetOrCreate(specification, this.innerRepo.Single);

    public async ValueTask<IEnumerable<T>> ListAsync(ISpecification<T> specification)
      => await this.cache.GetOrCreateAsync<IEnumerable<T>>(specification,
        async (spec) => await this.innerRepo.ListAsync(specification));
    
    public async ValueTask<T?> SingleAsync(ISpecification<T> specification)
      => await this.cache.GetOrCreateAsync<T?>(specification,
        async (spec) => await this.innerRepo.SingleAsync(specification));

    //public ValueTask SaveChangesAsync()
    //  => innerRepo.SaveChangesAsync();
  }
}
