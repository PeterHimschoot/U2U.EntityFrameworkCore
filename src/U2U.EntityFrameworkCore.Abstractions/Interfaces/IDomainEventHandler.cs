using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.EntityFrameworkCore.Abstractions.Interfaces
{
  public interface IDomainEventHandler
  {
    ValueTask Handle(object @event);
  }

  public interface IDomainEventHandler<T> : IDomainEventHandler where T : IDomainEvent
  {
  }
}
