namespace U2U.EntityFrameworkCore.TestData;

public class Login : EntityBase
{
  public Login(int id) : base(id) { }
  public string Provider { get; set; } = default!;
  public Student Student { get; set; } = default!;
  public int StudentId { get; set; }
}

