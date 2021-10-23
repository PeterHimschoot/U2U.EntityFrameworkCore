namespace U2U.EntityFrameworkCore.TestData;

public class Course : EntityBase
{
  public Course(int id) : base(id) { }
  public string Code { get; set; } = default!;
  public string Name { get; set; } = default!;
  public List<Session> Sessions { get; set; } = new ();
}

