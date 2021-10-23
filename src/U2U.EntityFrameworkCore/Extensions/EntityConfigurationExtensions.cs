namespace U2U.EntityFrameworkCore.Sql;

public static class EntityConfigurationExtensions
{
  public static EntityTypeBuilder<T> HasMaintenance<T>(this EntityTypeBuilder<T> entity, string created = CreationInspector<T>.UtcCreated, string modified = ModifiedInspector<T>.UtcModified)
    where T : class
  {
    entity
      .Property<DateTime>(created)
      .HasDefaultValueSql("GETUTCDATE()");

    entity
       .Property<DateTime>(modified)
      .HasDefaultValueSql("GETUTCDATE()");

    return entity;
  }
}
