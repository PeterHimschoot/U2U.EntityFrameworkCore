namespace U2U.EntityFrameworkCore.TestData;

public class LoginTestData : IEntityTestDataConfiguration<Login>
{
  public object[] TestData => new Login[]
  {
    new Login(1) { Provider = "X", StudentId = 1 },
    new Login(2) { Provider = "X", StudentId = 2 },
    new Login(3) { Provider = "X", StudentId = 3 }
  };
}
