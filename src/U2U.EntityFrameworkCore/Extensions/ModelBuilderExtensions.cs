namespace U2U.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
  public static void HasData<T>(this ModelBuilder modelBuilder, IEntityTypeData<T> config)
    where T : class
    => config.HasData(modelBuilder.Entity<T>());
}