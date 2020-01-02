using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace U2U.EntityFrameworkCore.Abstractions.Interfaces
{
  public interface IEntityInspector
  {
    Type TypeToInspect { get; }

    bool Inspect(EntityEntry entry, DateTime timestamp);
  }

  public interface IEntityInspectors
  {
    IEnumerable<IEntityInspector> All { get; }
  }
}
