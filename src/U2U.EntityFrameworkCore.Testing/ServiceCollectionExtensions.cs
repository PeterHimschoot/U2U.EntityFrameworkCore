using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

#nullable enable 

namespace U2U.Testing.Abstractions
{
  public static class ServiceCollectionExtensions
  {
    private static IServiceCollection Replace<I, C>(this IServiceCollection services, ServiceLifetime lifeTime)
      => services.Replace(new ServiceDescriptor(typeof(I), typeof(C), lifeTime));

    public static IServiceCollection ReplaceSingleton<I, C>(this IServiceCollection services)
    => services.Replace<I, C>(lifeTime: ServiceLifetime.Singleton);

    public static IServiceCollection ReplaceSingleton<I>(this IServiceCollection services, I instance)
    => services.Replace(new ServiceDescriptor(typeof(I), instance));

    public static IServiceCollection ReplaceTransient<I, C>(this IServiceCollection services)
    => services.Replace<I, C>(lifeTime: ServiceLifetime.Transient);

    public static IServiceCollection ReplaceScoped<I, C>(this IServiceCollection services)
    => services.Replace<I, C>(lifeTime: ServiceLifetime.Scoped);
  }
}
