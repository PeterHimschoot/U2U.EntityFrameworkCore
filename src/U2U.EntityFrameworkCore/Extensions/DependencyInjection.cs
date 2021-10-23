namespace U2U.EntityFrameworkCore;

public static class DependencyInjection
{
  public static IServiceCollection AddMaintenanceInspectors<T>(this IServiceCollection services, string creationPropertyName = CreationInspector<T>.UtcCreated, string modifiedPropertyName = ModifiedInspector<T>.UtcModified)
    where T : class
  => services.AddSingleton<IEntityInspector<T>>(new CreationInspector<T>(creationPropertyName))
             .AddSingleton<IEntityInspector<T>>(new ModifiedInspector<T>(modifiedPropertyName));

}

