using System.Collections.Generic;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore.TestData
{
  public class Course : EntityBase
  {
    public Course(int id) : base(id) { }
    public string Code { get; set; }
    public string Name { get; set; }
    public List<Session> Sessions { get; set; }
  }
}
