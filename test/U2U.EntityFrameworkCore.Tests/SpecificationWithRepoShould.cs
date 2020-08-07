#nullable enable

using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.EntityFrameworkCore.TestData;
using U2U.EntityFrameworkCore.Testing;
using Xunit;

namespace U2U.EntityFrameworkCore.Tests
{
  public class SpecificationWithRepoShould
    : IClassFixture<DbInjectionFixture<TrainingDb>>
    , IClassFixture<SpecificationFactories>
    , IClassFixture<StudentSpecificationFactory>
    , IClassFixture<SpecificationFactory<Login>>
  {
    private readonly DbInjectionFixture<TrainingDb> testFixture;
    private readonly SpecificationFactories specificationFactories;

    public SpecificationWithRepoShould(DbInjectionFixture<TrainingDb> testFixture,
      SpecificationFactories specificationFactories)
    {
      this.testFixture = testFixture;
      this.specificationFactories = specificationFactories;
    }

    [Fact]
    public async ValueTask ReturnAllStudentsWithIncludesToLoginAndSessionAndCourse()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        var repo = new StudentRepository(dbContext);

        System.Collections.Generic.IEnumerable<Student> allStudents =
          await repo.ListAsync(this.specificationFactories.For<Student>()
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
          student.Login.Should().NotBeNull();
          student.Sessions.Should().NotBeNull();
          foreach (StudentSession session in student.Sessions)
          {
            session.Session.Should().NotBeNull();
            session.Session.Course.Should().NotBeNull();
          }
        }
      }
    }

    [Fact]
    public async ValueTask ReturnProperIncludes()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        IReadonlyRepository<Login> repo = new Repository<Login, TrainingDb>(dbContext);

        ISpecification<Login> spec =
          this.specificationFactories.For<Login>()
            .WithId(1)
            .Include(l => l.Student);
        Login? login = await repo.SingleAsync(spec);
        login.Should().NotBeNull();
        login!.Student.Should().NotBeNull();
      }
    }

    [Fact]
    public async ValueTask WorkWithCachedSpecification()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();

        IOptions<MemoryCacheOptions> memCacheOptions =
          Options.Create<MemoryCacheOptions>(new MemoryCacheOptions());
        var memoryCache = new MemoryCache(memCacheOptions);

        IReadonlyRepository<Login> repo =
          new CachedRepository<Login, TrainingDb>(dbContext, memoryCache);

        ISpecification<Login> spec =
          this.specificationFactories.For<Login>()
          .Where(l => l.Id == 1)
          .Include(l => l.Student)
          .Not()
          .AsCached(TimeSpan.FromHours(12), 1);

        System.Collections.Generic.IEnumerable<Login> logins = await repo.ListAsync(spec);
        logins.Should().HaveCount(2);
        logins.Select(l => l.Student).Should().NotBeNull();

        // Let's create a new repository instance with the same cache
        // I would expect in this case that the repository returns the same instances
        // if they have been retrieved from the cache.

        repo =
          new CachedRepository<Login, TrainingDb>(dbContext, memoryCache);

        System.Collections.Generic.IEnumerable<Login> cachedLogins = await repo.ListAsync(spec);
        cachedLogins.Should().HaveCount(2);
        cachedLogins.Should().BeSameAs(logins);
      }
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
    public async ValueTask ReturnAllStudentsSpecificationWithIncludesToLoginAndSessionAndCourse()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        var repo = new StudentRepository(dbContext);

        var specification = new AllStudentsSpecification();
        System.Collections.Generic.IEnumerable<Student> allStudents =
          await repo.ListAsync(specification
                                 .Including(st => st.Login));

        allStudents.All(student => specification.Test(student)).Should().BeTrue();

        allStudents.Should().HaveCount(3);

        allStudents.Select(st => st.Login)
                   .Should().NotBeNull();
        allStudents.Select(st => st.Sessions)
                   .Should().NotBeNull();
        allStudents.SelectMany(st => st.Sessions)
                   .Select(ss => ss.Session)
                   .Select(s => s.Course).Should().NotBeNull();

        foreach (Student student in allStudents)
        {
          student.Login.Should().NotBeNull();
          student.Sessions.Should().NotBeNull();
          foreach (StudentSession session in student.Sessions)
          {
            session.Session.Should().NotBeNull();
            session.Session.Course.Should().NotBeNull();
          }
        }
      }
    }

    [Fact]
    public async ValueTask ReturnStudentWithIdSpecificationWithIncludesToSessionAndCourse()
    {
      const string dbName = "FakeDatabase";

      using (TrainingDb dbContext = this.testFixture.CreateInMemoryDbContext(dbName))
      {
        dbContext.Database.EnsureCreated();
        var repo = new StudentRepository(dbContext);

        Student? student =
          await repo.SingleAsync(new StudentWithIdSpecification(1)
                                 .Including(st => st.Login));
        student.Should().NotBeNull();
        student!.Login
                .Should().BeNull();
        student!.Sessions
                   .Should().NotBeNull();
        student!.Sessions
               .Select(ss => ss.Session)
               .Select(s => s.Course).Should().NotBeNull();
      }
    }
  }
}
