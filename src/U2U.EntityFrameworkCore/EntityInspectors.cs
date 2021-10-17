namespace U2U.EntityFrameworkCore;

public class EntityInspectors : IEntityInspectors
{
  public static readonly IEntityInspectors None = new EntityInspectors(Enumerable.Empty<IEntityInspector>());

  private readonly IEnumerable<IEntityInspector> allInspectors;

  public EntityInspectors(IEnumerable<IEntityInspector> allInspectors)
    => this.allInspectors = allInspectors;

  public IEnumerable<IEntityInspector> All
    => this.allInspectors;
}