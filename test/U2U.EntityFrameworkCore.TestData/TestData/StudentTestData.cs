namespace U2U.EntityFrameworkCore.TestData;

public class StudentTestData : IEntityTestDataConfiguration<Student>
{
  public object[] TestData => new Student[]
  {
    new Student(1) { FirstName = "Joske", LastName = "Vermeulen" },
    new Student(2) { FirstName = "Eddy", LastName = "Wally" },
    new Student(3) { FirstName = "Sam", LastName = "Goris" }
  };
}

