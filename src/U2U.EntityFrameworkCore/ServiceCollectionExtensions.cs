using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using U2U.EntityFrameworkCore.Abstractions.Interfaces;

namespace U2U.EntityFrameworkCore
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddU2UEntityFrameworkCore(this IServiceCollection services)
      => services.AddSingleton<IEntityInspectors, EntityInspectors>();
  }
}
