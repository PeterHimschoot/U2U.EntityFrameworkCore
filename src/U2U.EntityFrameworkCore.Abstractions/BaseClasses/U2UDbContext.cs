#nullable enable

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using U2U.EntityFrameworkCore.Abstractions.Interfaces;

namespace U2U.EntityFrameworkCore.Abstractions.BaseClasses
{
  public class U2UDbContext : DbContext
  {
    private readonly IEntityInspectors? inspectors;

    public U2UDbContext() : base()
    {
      this.inspectors = null;
    }

    public U2UDbContext(DbContextOptions options, IEntityInspectors inspectors) : base(options)
    {
      this.inspectors = inspectors;
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
      if (inspectors != null)
      {
        DateTime now = DateTime.Now;
        foreach (var entry in this.ChangeTracker.Entries())
        {
          foreach (var inspector in inspectors.All)
          {
            if (entry.Entity.GetType() == inspector.TypeToInspect)
            {
              inspector.Inspect(entry, now);
            }
          }
        }
      }

      return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
  }
}
