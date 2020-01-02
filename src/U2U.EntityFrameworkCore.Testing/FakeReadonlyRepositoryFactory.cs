using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using U2U.EntityFrameworkCore.Abstractions;

#nullable enable 

namespace U2U.EntityFrameworkCore.Testing
{
  public class FakeRepositoryFactory<T> where T : class
  {
    public IRepository<T> FakeRepoWithData(IQueryable<T> data, Action<Mock<IRepository<T>>>? setup = null)
    {
      var repoMock = new Mock<IRepository<T>>();

      repoMock
        .Setup(m => m.Single(It.IsAny<ISpecification<T>>()))
          .Returns((ISpecification<T> spec) =>
            spec.WithFakeData(data).SingleOrDefault(spec.Criteria));

      repoMock
        .Setup(m => m.SingleAsync(It.IsAny<ISpecification<T>>()))
          .ReturnsAsync((ISpecification<T> spec) =>
            spec.WithFakeData(data).SingleOrDefault(spec.Criteria));

      repoMock
        .Setup(m => m.List(It.IsAny<ISpecification<T>>()))
          .Returns((ISpecification<T> spec) =>
            spec.WithFakeData(data).Where(spec.Criteria).AsEnumerable());

      repoMock.Setup(m => m.ListAsync(It.IsAny<ISpecification<T>>()))
          .ReturnsAsync((ISpecification<T> spec) =>
            spec.Includes.Aggregate(seed: spec.WithFakeData(data),
                                    func: (current, include) => current.Include(include))
                .Where(spec.Criteria)
                .AsEnumerable());

      repoMock.Setup(m => m.SingleAsync(It.IsAny<ISpecification<T>>()))
        .ReturnsAsync((ISpecification<T> spec) =>
            spec.Includes.Aggregate(seed: spec.WithFakeData(data),
                                    func: (current, include) => current.Include(include))
                .SingleOrDefault(spec.Criteria));

      setup?.Invoke(repoMock);

      return repoMock.Object;
    }
  }
}
