using System.Collections.Generic;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore.TestData
{
  public class Student : EntityBase
  {
    public Student(int id) : base(id) { }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Login Login { get; set; }
    public List<StudentSession> Sessions { get; set; }
  }
}
