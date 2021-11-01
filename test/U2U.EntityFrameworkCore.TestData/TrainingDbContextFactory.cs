namespace U2U.EntityFrameworkCore.TestData;

public class TrainingDbContextFactory
{
  public class GamesDbContextFactory 
    : IDesignTimeDbContextFactory<TrainingDb>
  {
    public TrainingDb CreateDbContext(string[] args)
    {
      // This project should use the same user secrets key as Clean.Architecture.Web.csproj !!!
      var cb = new ConfigurationBuilder();
      cb.AddUserSecrets<GamesDbContextFactory>();

      IConfigurationRoot configuration = cb.Build();
      string connectionString = configuration.GetConnectionString("TrainingDb");

      var builder = new DbContextOptionsBuilder<TrainingDb>();
      builder.UseSqlServer(connectionString);
      builder.EnableSensitiveDataLogging();
      return new TrainingDb(builder.Options);
    }
  }
}
