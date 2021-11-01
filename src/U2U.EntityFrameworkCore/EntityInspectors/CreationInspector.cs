namespace U2U.EntityFrameworkCore;

public class CreationInspector<T> : IEntityInspector<T>
{
  public const string UtcCreated = "UtcCreated";

  private readonly string propertyName;

  public CreationInspector(string propertyName = "UtcCreated") 
    => this.propertyName = propertyName;

  public void Inspect(InspectorContext context)
  {
    if (context.Entry.Entity is T && context.Entry.State == EntityState.Added)
    {
      context.Entry.Property(this.propertyName).CurrentValue = context.TimeStamp;
    }
  }
}

