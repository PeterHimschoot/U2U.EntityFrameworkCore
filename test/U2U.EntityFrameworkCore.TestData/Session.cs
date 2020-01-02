using System;
using System.Collections.Generic;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore.TestData
{
  public class Session : EntityBase
  {
    public Session(int id) : base(id) { }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Course Course { get; set; }
    public int CourseId { get; set; }
    public List<StudentSession> Students { get; set; }
  }
}
