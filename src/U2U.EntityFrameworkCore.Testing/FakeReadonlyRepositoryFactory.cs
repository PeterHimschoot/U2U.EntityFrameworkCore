using NSubstitute;

namespace U2U.EntityFrameworkCore.Testing;

public class FakeRepositoryFactory<T> where T : class, IAggregateRoot
{
  public IRepository<T> FakeRepoWithData(IQueryable<T> data, Action<IRepository<T>>? setup = null)
  {
    var repoMock = Substitute.For<IRepository<T>>();

    repoMock
      .SingleAsync(Arg.Any<ISpecification<T>>())
      .Returns((args) =>
      {
        ISpecification<T> spec = (ISpecification<T>)args[0];
        return spec.WithFakeData(data).SingleOrDefault(spec.Criteria);
      });

    repoMock
      .ListAsync(Arg.Any<ISpecification<T>>())
        .Returns((args) =>
        {
          ISpecification<T> spec = (ISpecification<T>)args[0];
          return spec.Includes.Aggregate(seed: spec.WithFakeData(data),
                                         func: (current, include) => current.Include(include))
               .Where(spec.Criteria)
               .AsEnumerable();
        });

    repoMock.SingleAsync(Arg.Any<ISpecification<T>>())
      .Returns((args) =>
      {
        ISpecification<T> spec = (ISpecification<T>)args[0];


        return spec.Includes.Aggregate(seed: spec.WithFakeData(data),
                                func: (current, include) => current.Include(include))
            .SingleOrDefault(spec.Criteria);

      });
    setup?.Invoke(repoMock);
    return repoMock;
  }
}

