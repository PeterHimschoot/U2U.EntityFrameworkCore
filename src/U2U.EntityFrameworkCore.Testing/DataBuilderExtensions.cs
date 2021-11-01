namespace U2U.EntityFrameworkCore.Testing;

public interface IEntityTestDataConfiguration<T> where T : class
{
  object[] TestData { get; }
}

public static class ModelBuilderExtensions
{
  public static void ApplyTestData<T>(
    this ModelBuilder builder,
    IEntityTestDataConfiguration<T> config)
    where T : class => builder.Entity<T>().HasData(config.TestData);
}
