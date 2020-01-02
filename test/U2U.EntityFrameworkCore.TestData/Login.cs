using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore.TestData
{
  public class Login : EntityBase
  {
    public Login(int id) : base(id) { }
    public string Provider { get; set; }
    public Student Student { get; set; }
    public int StudentId { get; set; }
  }
}
