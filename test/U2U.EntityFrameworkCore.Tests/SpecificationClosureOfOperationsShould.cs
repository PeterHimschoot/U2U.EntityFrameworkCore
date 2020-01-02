#nullable enable

using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.EntityFrameworkCore.TestData;
using U2U.EntityFrameworkCore.Testing;
using Xunit;

namespace U2U.EntityFrameworkCore.Tests
{
  public class SpecificationClosureOfOperationsShould
    : IClassFixture<DbInjectionFixture<TrainingDb>>
    , IClassFixture<SpecificationFactories>
  {
    private readonly DbInjectionFixture<TrainingDb> testFixture;
    private readonly SpecificationFactories specificationFactories;

    public SpecificationClosureOfOperationsShould(DbInjectionFixture<TrainingDb> testFixture,
      SpecificationFactories specificationFactories)
    {
      this.testFixture = testFixture;
      this.specificationFactories = specificationFactories;
    }

    [Fact]
    public async ValueTask WorkWithAndSpecification()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

        ISpecification<Login> left = this.specificationFactories.For<Login>().Where(l => l.Id >= 1);
        ISpecification<Login> right = this.specificationFactories.For<Login>().Where(l2 => l2.Id <= 2);
        ISpecification<Login> spec = left.Include(l => l.Student).And(right);

        System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
        logins.Should().HaveCount(2);
        logins.Select(l => l.Student).Should().NotBeNull();
      }
    }

    [Fact]
    public async ValueTask WorkWithAndExpression()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

        ISpecification<Login> spec =
          this.specificationFactories.For<Login>().Where(l => l.Id >= 1)
                                             .And(l2 => l2.Id <= 2)
                                             .Include(l => l.Student);

        System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
        logins.Should().HaveCount(2);
        logins.Select(l => l.Student).Should().NotBeNull();
      }
    }

    [Fact]
    public async ValueTask WorkWithOrSpecification()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

        ISpecification<Login> right =
          this.specificationFactories.For<Login>().Where(l2 => l2.Id == 2);

        ISpecification<Login> spec = this.specificationFactories.For<Login>()
          .Where(l => l.Id == 1)
          .Or(right)
          .Include(l => l.Student);

        System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
        logins.Should().HaveCount(2);
        logins.Select(l => l.Student).Should().NotBeNull();
      }
    }

    [Fact]
    public async ValueTask WorkWithOrExpression()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

        ISpecification<Login> spec =
          this.specificationFactories.For<Login>()
          .Where(l => l.Id == 1)
          .Include(l => l.Student)
          .Or(l => l.Id == 2);

        System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
        logins.Should().HaveCount(2);
        logins.Select(l => l.Student).Should().NotBeNull();
      }
    }

    [Fact]
    public async ValueTask WorkWithNotSpecification()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

        ISpecification<Login> spec =
          this.specificationFactories.For<Login>()
          .Where(l => l.Id == 1)
          .Include(l => l.Student)
          .Not();

        System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
        logins.Should().HaveCount(2);
        logins.Select(l => l.Student).Should().NotBeNull();
      }
    }
  }
}
