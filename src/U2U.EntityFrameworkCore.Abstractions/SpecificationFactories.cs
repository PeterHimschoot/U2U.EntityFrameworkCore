namespace U2U.EntityFrameworkCore;

/// <summary>
/// Easy way to group a bunch of specificationFactories
/// </summary>
public class SpecificationFactories
{
  private readonly Dictionary<Type, ISpecificationFactory> factories =
    new Dictionary<Type, ISpecificationFactory>();

  internal void Register(Type t, ISpecificationFactory factory)
    => this.factories.Add(t, factory);

  internal ISpecificationFactory? For(Type t)
  {
    if (this.factories.TryGetValue(t, out ISpecificationFactory? factory))
    {
      return factory;
    }
    else
    {
      return null;
    }
  }
}

public static class SpecificationFactoriesExtensions
{
  public static SpecificationFactories Add<T>(this SpecificationFactories factories, SpecificationFactory<T> factory)
    where T : class//, IEntity
  {
    factories.Register(typeof(T), factory);
    return factories;
  }

  public static R For<T, R>(this SpecificationFactories factories)
    where T : class//, IEntity
    where R : SpecificationFactory<T>
  {
    ISpecificationFactory? factory = factories.For(typeof(T));
    if (factory == null)
    {
      var autoFac = new SpecificationFactory<T>(); // Nothing to do with AutoFac DI :)
      factories.Add<T>(autoFac);
      return (R)autoFac;
    }
    return (R)factory;
  }

  public static SpecificationFactory<T> For<T>(this SpecificationFactories factories)
    where T : class//, IEntity
  => For<T, SpecificationFactory<T>>(factories);
}

