#nullable enable

using System.Collections;
using System.Collections.Generic;
using U2U.EntityFrameworkCore.Abstractions.Interfaces;

namespace U2U.EntityFrameworkCore.Abstractions
{
  /// <summary>
  /// EntityBase serves as the base class for all entities.
  /// </summary>
  public class EntityBase
  {
    public EntityBase(int id)
      => Id = id;

    public int Id { get; }

    public ICollection<IDomainEvent>? domainEvents;

    protected void RegisterDomainEvent(IDomainEvent @event)
    {
      domainEvents ??= new List<IDomainEvent>();
      domainEvents.Add(@event);
    }
  }
}
