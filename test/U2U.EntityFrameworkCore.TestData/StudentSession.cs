namespace U2U.EntityFrameworkCore.TestData
{
  public class StudentSession
  {
    public Session Session { get; set; }
    public int SessionId { get; set; }
    public Student Student { get; set; }
    public int StudentId { get; set; }
  }
}
