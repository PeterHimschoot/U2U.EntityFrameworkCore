namespace U2U.EntityFrameworkCore.Abstractions;
/// <summary>
/// EntityBase serves as the base class for all entities.
/// However, this is not required for all entities.
/// </summary>
public class EntityBase
{
  public EntityBase(int id)
    => Id = id;

  public int Id { get; }

  public ICollection<IDomainEvent>? domainEvents;

  protected void RegisterDomainEvent(IDomainEvent @event)
  {
    if (this.domainEvents is null)
    {
      this.domainEvents = new List<IDomainEvent>();
    }
    this.domainEvents.Add(@event);
  }
}
