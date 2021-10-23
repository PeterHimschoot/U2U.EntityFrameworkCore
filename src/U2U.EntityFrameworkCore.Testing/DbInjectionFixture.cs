namespace U2U.EntityFrameworkCore.Testing;

public class DbInjectionFixture<TDb> where TDb : DbContext
{
  public TransactionScope StartTest()
    => new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(30), TransactionScopeAsyncFlowOption.Enabled);

  private readonly IServiceCollection services = new ServiceCollection();
  private IServiceProvider? serviceProvider = null;

  public DbInjectionFixture<TDb> ConfigureServices(Action<IServiceCollection> configure)
  {
    configure(services);
    return this;
  }

  protected virtual void ConfigureServices(IServiceCollection services)
  {
  }

  //private void AddDefaultEntityFrameworkDatabases(IServiceCollection services)
  //{
  //  services.AddEntityFrameworkInMemoryDatabase();
  //  services.AddEntityFrameworkSqlServer();
  //  services.AddEntityFrameworkSqlite();
  //}

  public TDb CreateDbContext(string model, string nameOfConnectionString, ITestOutputHelper? output = null)
  {
    if (serviceProvider is null)
    {
      ConfigureServices(services);

      string connectionString = DbConfigurationBuilder<TDb>.GetConnectionString(nameOfConnectionString);
      services.AddDbContext<TDb>((serviceProvider, options) =>
      {
        options.UseInternalServiceProvider(serviceProvider);
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
      serviceProvider = services.BuildServiceProvider();
    }

    TDb? dbContext = serviceProvider.GetRequiredService<TDb>();
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

