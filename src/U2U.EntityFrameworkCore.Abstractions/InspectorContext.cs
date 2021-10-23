namespace U2U.EntityFrameworkCore.Abstractions;

public struct InspectorContext
{
  public EntityEntry Entry { get; }
  public DateTime TimeStamp { get; }

  public InspectorContext(EntityEntry entry, DateTime timeStamp)
  {
    Entry = entry;
    TimeStamp = timeStamp;
  }
}

