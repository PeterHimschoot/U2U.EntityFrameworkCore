using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace U2U.EntityFrameworkCore.TestData
{
  public class StudentRepository : Repository<Student, TrainingDb>
  {
    public StudentRepository(TrainingDb db)
  : base(db)
    { }

    protected override IQueryable<Student> Includes(IQueryable<Student> q)
      => q.Include(student => student.Login)
          .Include(student => student.Sessions)
          .ThenInclude(session => session.Session.Course);
  }
}
