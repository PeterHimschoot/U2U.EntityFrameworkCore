using Microsoft.Extensions.DependencyInjection;

namespace U2U.EntityFrameworkCore.Tests;

public class SpecificationClosureOfOperationsShould
  : IClassFixture<SpecificationFactories>
{
  private readonly SpecificationFactories specificationFactories;

  public SpecificationClosureOfOperationsShould(SpecificationFactories specificationFactories) 
  => this.specificationFactories = specificationFactories;

  private DbInjectionFixture<TrainingDb> InMemFixture()
  => new DbInjectionFixture<TrainingDb>()
  .ConfigureServices(services => services
  .AddEntityFrameworkInMemoryDatabase()
  //.AddMaintenanceInspectors<Game>()
  );

  [Fact]
  public async Task WorkWithAndSpecification()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

    ISpecification<Login> left = specificationFactories.For<Login>().Where(l => l.Id >= 1);
    ISpecification<Login> right = specificationFactories.For<Login>().Where(l2 => l2.Id <= 2);
    ISpecification<Login> spec = left.Include(l => l.Student).And(right);

    System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
    _ = logins.Should().HaveCount(2);
    _ = logins.Select(l => l.Student).Should().NotBeNull();
  }

  [Fact]
  public async Task WorkWithAndExpression()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

    ISpecification<Login> spec =
      specificationFactories.For<Login>().Where(l => l.Id >= 1)
                                         .And(l2 => l2.Id <= 2)
                                         .Include(l => l.Student);

    System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
    _ = logins.Should().HaveCount(2);
    _ = logins.Select(l => l.Student).Should().NotBeNull();
  }

  [Fact]
  public async Task WorkWithOrSpecification()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

    ISpecification<Login> right =
      specificationFactories.For<Login>().Where(l2 => l2.Id == 2);

    ISpecification<Login> spec = specificationFactories.For<Login>()
      .Where(l => l.Id == 1)
      .Or(right)
      .Include(l => l.Student);

    System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
    _ = logins.Should().HaveCount(2);
    _ = logins.Select(l => l.Student).Should().NotBeNull();
  }

  [Fact]
  public async Task WorkWithOrExpression()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

    ISpecification<Login> spec =
      specificationFactories.For<Login>()
      .Where(l => l.Id == 1)
      .Include(l => l.Student)
      .Or(l => l.Id == 2);

    System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
    _ = logins.Should().HaveCount(2);
    _ = logins.Select(l => l.Student).Should().NotBeNull();
  }

  [Fact]
  public async Task WorkWithNotSpecification()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

    ISpecification<Login> spec =
      specificationFactories.For<Login>()
      .Where(l => l.Id == 1)
      .Include(l => l.Student)
      .Not();

    System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
    _ = logins.Should().HaveCount(2);
    _ = logins.Select(l => l.Student).Should().NotBeNull();
  }
}

