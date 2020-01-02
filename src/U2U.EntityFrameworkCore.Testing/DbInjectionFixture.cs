using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Transactions;
using U2U.EntityFrameworkCore;
using U2U.EntityFrameworkCore.Abstractions;

#nullable enable

namespace U2U.EntityFrameworkCore.Testing
{
  public class DbInjectionFixture<TDb> where TDb : DbContext
  {
    readonly IServiceCollection services = new ServiceCollection();

    public TransactionScope StartTest()
      => new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(30), TransactionScopeAsyncFlowOption.Enabled);

    public TDb CreateDbContext(string model, string nameOfConnectionString) 
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
      });
      var dbContext = services.BuildServiceProvider().GetRequiredService<TDb>();
      return dbContext;
    }


    public TDb CreateSqlServerDbContext(string nameOfConnectionString)
      => CreateDbContext("SQL", nameOfConnectionString);

    public TDb CreateInMemoryDbContext(string nameOfConnectionString)
      => CreateDbContext("MEM", nameOfConnectionString);

    public TDb UseInMemoryDbContext(string nameOfConnectionString)
      => CreateDbContext("MEME", nameOfConnectionString);

    public TDb CreateSqliteDbContext(string nameOfConnectionString)
      => CreateDbContext("LIT", nameOfConnectionString);

  }
}
