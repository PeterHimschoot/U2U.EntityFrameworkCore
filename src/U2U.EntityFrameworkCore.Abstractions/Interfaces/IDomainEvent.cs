using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.EntityFrameworkCore.Abstractions.Interfaces
{
  public interface IDomainEvent { }

  public interface IDomainEvent<T> : IDomainEvent where T : EntityBase
  {

  }
}
