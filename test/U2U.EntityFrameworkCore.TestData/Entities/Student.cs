namespace U2U.EntityFrameworkCore.TestData;

public class Student : EntityBase, IAggregateRoot
{
  public Student(int id) : base(id) { }
  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
  public Login Login { get; set; } = default!;
  public List<StudentSession> Sessions { get; set; } = new();
}
