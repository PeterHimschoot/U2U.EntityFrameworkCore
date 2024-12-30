using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace U2U.EntityFrameworkCore.Tests;

public class SpecificationWithRepoShould
  : IClassFixture<SpecificationFactories>
  , IClassFixture<StudentSpecificationFactory>
  , IClassFixture<SpecificationFactory<Login>>
{
  private readonly SpecificationFactories specificationFactories;

  public SpecificationWithRepoShould(SpecificationFactories specificationFactories) => this.specificationFactories = specificationFactories;

  private DbInjectionFixture<TrainingDb> InMemFixture()
  => new DbInjectionFixture<TrainingDb>()
    .ConfigureServices(services => services
    .AddEntityFrameworkInMemoryDatabase()
    //.AddMaintenanceInspectors<Game>()
    );

  [Fact]
  public async Task ReturnAllStudentsWithIncludesToLoginAndSessionAndCourse()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    StudentRepository repo = new(dbContext);

    System.Collections.Generic.IEnumerable<Student> allStudents =
      await repo.ListAsync(specificationFactories.For<Student>()
                                                 .All()
                                                 .Including(st => st.Login));
    _ = allStudents.Should().HaveCount(3);

    _ = allStudents.Select(st => st.Login)
               .Should().NotBeNull();
    _ = allStudents.Select(st => st.Sessions)
               .Should().NotBeNull();
    _ = allStudents.SelectMany(st => st.Sessions)
               .Select(ss => ss.Session)
               .Select(s => s.Course).Should().NotBeNull();

    foreach (Student student in allStudents)
    {
      _ = student.Login.Should().NotBeNull();
      _ = student.Sessions.Should().NotBeNull();
      foreach (StudentSession session in student.Sessions)
      {
        _ = session.Session.Should().NotBeNull();
        _ = session.Session.Course.Should().NotBeNull();
      }
    }
  }

  [Fact]
  public async Task ReturnProperIncludes()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

    ISpecification<Login> spec =
      specificationFactories.For<Login>()
        .WithId(1)
        .Include(l => l.Student);
    Login? login = await repo.SingleAsync(spec);
    _ = login.Should().NotBeNull();
    _ = login!.Student.Should().NotBeNull();
  }

  [Fact]
  public async Task WorkWithCachedSpecification()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();

    IOptions<MemoryCacheOptions> memCacheOptions =
      Options.Create<MemoryCacheOptions>(new MemoryCacheOptions());
    MemoryCache memoryCache = new(memCacheOptions);

    IReadonlyRepository<Login> repo =
      new CachedRepository<Login, TrainingDb>(dbContext, memoryCache);

    ISpecification<Login> spec =
      specificationFactories.For<Login>()
      .Where(l => l.Id == 1)
      .Include(l => l.Student)
      .Not()
      .AsCached(TimeSpan.FromHours(12), 1);

    System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
    _ = logins.Should().HaveCount(2);
    _ = logins.Select(l => l.Student).Should().NotBeNull();

    // Let's create a new repository instance with the same cache
    // I would expect in this case that the repository returns the same instances
    // if they have been retrieved from the cache.

    repo =
      new CachedRepository<Login, TrainingDb>(dbContext, memoryCache);

    System.Collections.Generic.IEnumerable<Login> cachedLogins = await repo.ListAsync(spec);
    _ = cachedLogins.Should().HaveCount(2);
    _ = cachedLogins.Should().BeSameAs(logins);
  }

  public class AllStudentsSpecification : Specification<Student>
  {
    public AllStudentsSpecification()
      : base(student => true)
    { }
  }

  public class StudentWithIdSpecification : Specification<Student>
  {
    public StudentWithIdSpecification(int id)
      : base(student => student.Id == id)
    { }
  }

  public class StudentWithIdSpecificationCached : CachedSpecification<Student, int>
  {
    public StudentWithIdSpecificationCached(int id)
      : base(criteria: student => student.Id == id,
          cacheDuration: TimeSpan.FromHours(1),
          key: id)
    { }
  }

  public class StudentWithIdAndLoginSpecification : StudentWithIdSpecification
  {
    public StudentWithIdAndLoginSpecification(int id)
      : base(id) => Include(student => student.Login);
  }

  [Fact]
  public async Task ReturnAllStudentsSpecificationWithIncludesToLoginAndSessionAndCourse()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    StudentRepository repo = new(dbContext);

    AllStudentsSpecification specification = new();
    IEnumerable<Student> allStudents =
      await repo.ListAsync(specification
                             .Including(st => st.Login));

    _ = allStudents.All(student => specification.Test(student)).Should().BeTrue();

    _ = allStudents.Should().HaveCount(3);

    _ = allStudents.Select(st => st.Login)
               .Should().NotBeNull();
    _ = allStudents.Select(st => st.Sessions)
               .Should().NotBeNull();
    _ = allStudents.SelectMany(st => st.Sessions)
               .Select(ss => ss.Session)
               .Select(s => s.Course).Should().NotBeNull();

    foreach (Student student in allStudents)
    {
      _ = student.Login.Should().NotBeNull();
      _ = student.Sessions.Should().NotBeNull();
      foreach (StudentSession session in student.Sessions)
      {
        _ = session.Session.Should().NotBeNull();
        _ = session.Session.Course.Should().NotBeNull();
      }
    }
  }

  [Fact]
  public async Task ReturnStudentWithIdSpecificationWithIncludesToSessionAndCourse()
  {
    const string dbName = "FakeDatabase";

    using TrainingDb dbContext = InMemFixture().CreateInMemoryDbContext(dbName);
    _ = dbContext.Database.EnsureCreated();
    StudentRepository repo = new(dbContext);

    Student? student =
      await repo.SingleAsync(new StudentWithIdSpecification(1)
                             .Including(st => st.Login));
    _ = student.Should().NotBeNull();
    _ = student!.Login
            .Should().NotBeNull();
    _ = student!.Sessions
               .Should().NotBeNull();
    _ = student!.Sessions
           .Select(ss => ss.Session)
           .Select(s => s.Course).Should().NotBeNull();
  }
}
