namespace U2U.EntityFrameworkCore.TestData;

public class SessionTestData : IEntityTestDataConfiguration<Session>
{
  public object[] TestData => new Session[]
  {
    new Session(1) { CourseId = 1, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
    new Session(2) { CourseId = 2, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
    new Session(3) { CourseId = 3, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
    new Session(4) { CourseId = 1, StartDate = new DateTime(2020, 2, 6), EndDate = new DateTime(2020, 2, 10) },
    new Session(5) { CourseId = 2, StartDate = new DateTime(2020, 3, 6), EndDate = new DateTime(2020, 3, 10) },
    new Session(6) { CourseId = 3, StartDate = new DateTime(2020, 4, 6), EndDate = new DateTime(2020, 4, 10) },
    new Session(7) { CourseId = 1, StartDate = new DateTime(2020, 5, 6), EndDate = new DateTime(2020, 5, 10) },
    new Session(8) { CourseId = 2, StartDate = new DateTime(2020, 6, 6), EndDate = new DateTime(2020, 6, 10) },
    new Session(9) { CourseId = 3, StartDate = new DateTime(2020, 7, 6), EndDate = new DateTime(2020, 7, 10) }
  };
}
