namespace U2U.EntityFrameworkCore;

public class ModifiedInspector<T> : IEntityInspector<T>
{
  public const string UtcModified = "UtcModified";

  private readonly string propertyName;

  public ModifiedInspector(string propertyName = UtcModified) => this.propertyName = propertyName;

  public void Inspect(InspectorContext context)
  {
    if (context.Entry.Entity is T && (context.Entry.State == EntityState.Added || context.Entry.State == EntityState.Modified))
    {
      context.Entry.Property(this.propertyName).CurrentValue = context.TimeStamp;
    }
  }
}

