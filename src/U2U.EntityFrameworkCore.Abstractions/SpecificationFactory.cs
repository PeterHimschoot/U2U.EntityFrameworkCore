namespace U2U.EntityFrameworkCore;

public class SpecificationFactory<T>
    : ISpecificationFactory<T>
    where T : class
{
  // Flyweights
  private readonly ISpecification<T> none = new Specification<T>(t => false);
  private readonly ISpecification<T> all = new Specification<T>(t => true);

  public ISpecification<T> None() => this.none;

  public ISpecification<T> All() => this.all;

  public ISpecification<T> Where(Expression<Func<T, bool>> condition)
    => new Specification<T>(condition);

  public virtual bool ForType(Type t)
    => typeof(T) == t;
}

public static class SpecificationFactoryExtensions
{
  public static ISpecification<T> WithId<T>(this ISpecificationFactory<T> _, int id)
    where T : EntityBase
    => new EntitySpecification<T>(id);

}

