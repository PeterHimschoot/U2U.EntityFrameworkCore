namespace U2U.EntityFrameworkCore.Testing;

public class DbInjectionFixture<TDb> where TDb : DbContext
{
  //private readonly IServiceCollection services = new ServiceCollection();

  public TransactionScope StartTest()
    => new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(30), TransactionScopeAsyncFlowOption.Enabled);

  public TDb CreateDbContext(string model, string nameOfConnectionString, ITestOutputHelper? output = null)
  {
    var services = new ServiceCollection();
    string connectionString = DbConfigurationBuilder<TDb>.GetConnectionString(nameOfConnectionString);
    services.AddDbContext<TDb>(options =>
    {
      if (model == "SQL")
      {
        options.UseSqlServer(connectionString);
      }
      if (model == "MEM")
      {
        options.UseInMemoryDatabase(connectionString, new InMemoryDatabaseRoot());
      }
      if (model == "MEME")
      {
        options.UseInMemoryDatabase(connectionString);
      }
      if (model == "LIT")
      {
        options.UseSqlite(connectionString);
      }
      options.EnableSensitiveDataLogging();

      if (output != null)
      {

        var provider = new TestOutputLoggerProvider(output);
        var loggerFactory = new LoggerFactory(new[]
        {
            provider
        });

        options.UseLoggerFactory(loggerFactory);
      }

    });
    TDb? dbContext = services.BuildServiceProvider().GetRequiredService<TDb>();
    return dbContext;
  }

  public TDb CreateSqlServerDbContext(string nameOfConnectionString, ITestOutputHelper? output = null)
    => CreateDbContext("SQL", nameOfConnectionString, output);

  public TDb CreateInMemoryDbContext(string nameOfConnectionString, ITestOutputHelper? output = null)
    => CreateDbContext("MEM", nameOfConnectionString, output);

  public TDb UseInMemoryDbContext(string nameOfConnectionString, ITestOutputHelper? output = null)
    => CreateDbContext("MEME", nameOfConnectionString, output);

  public TDb CreateSqliteDbContext(string nameOfConnectionString, ITestOutputHelper? output = null)
    => CreateDbContext("LIT", nameOfConnectionString, output);

}

