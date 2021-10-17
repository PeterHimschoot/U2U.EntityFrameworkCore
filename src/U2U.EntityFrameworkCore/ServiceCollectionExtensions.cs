namespace U2U.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddU2UEntityFrameworkCore(this IServiceCollection services)
    => services.AddSingleton<IEntityInspectors, EntityInspectors>();
}

