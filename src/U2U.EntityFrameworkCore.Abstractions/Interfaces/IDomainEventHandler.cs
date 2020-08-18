using System.Threading.Tasks;

namespace U2U.EntityFrameworkCore.Abstractions.Interfaces
{
  public interface IDomainEventHandler
  {
    ValueTask Handle(object @event);
  }

  public interface IDomainEventHandler<TDomainEvent> : IDomainEventHandler 
    where TDomainEvent : IDomainEvent
  {
  }
}
