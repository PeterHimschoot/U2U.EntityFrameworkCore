namespace U2U.EntityFrameworkCore.TestData;

public class CourseTestData : IEntityTestDataConfiguration<Course>
{
  public object[] TestData
    => new Course[] {
        new Course(1) { Code = "UARCH", Name = "Patterns and Practices" },
        new Course(2) { Code = "UWEBA", Name = "Advanced Web Development" },
        new Course(3) { Code = "UCORE", Name = "Upgrade to DotNet Core" },
        new Course(4) { Code = "UDEF", Name = "Domain Driven Design With Entity Framework Core" }
    };
}

