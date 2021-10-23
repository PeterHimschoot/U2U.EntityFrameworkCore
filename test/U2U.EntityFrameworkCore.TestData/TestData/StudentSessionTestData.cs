namespace U2U.EntityFrameworkCore.TestData;

public class StudentSessionTestData : IEntityTestDataConfiguration<StudentSession>
{
  public object[] TestData => new StudentSession[]
  {
    new StudentSession() { SessionId = 1, StudentId = 1 },
    new StudentSession() { SessionId = 1, StudentId = 3 },
    new StudentSession() { SessionId = 2, StudentId = 2 },
    new StudentSession() { SessionId = 3, StudentId = 1 },
    new StudentSession() { SessionId = 3, StudentId = 2 },
    new StudentSession() { SessionId = 3, StudentId = 3 }
  };
}
