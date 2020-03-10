using System;
using System.Collections.Generic;
using System.Text;
using U2U.EntityFrameworkCore.Abstractions;

namespace U2U.EntityFrameworkCore
{
  public class EntitySpecification<T> : Specification<T>
    where T : EntityBase
  {
    public EntitySpecification(int id) : base(t => t.Id == id) { }
  }
}
