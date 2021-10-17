namespace U2U.EntityFrameworkCore;

public static class SpecificationTestingExtensions
{
  public static IQueryable<T> WithFakeData<T>(this ISpecification<T> spec, IQueryable<T> data)
    where T : class// , IEntity
    => spec.BuildQueryable(data);
}

