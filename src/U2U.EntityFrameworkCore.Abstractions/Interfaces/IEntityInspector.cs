using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace U2U.EntityFrameworkCore.Abstractions.Interfaces;

public interface IEntityInspector
{
  Type TypeToInspect { get; }

  bool Inspect(EntityEntry entry, DateTime timestamp);
}

public interface IEntityInspectors
{
  IEnumerable<IEntityInspector> All { get; }
}

