#nullable enable

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace U2U.EntityFrameworkCore
{
  public static class DbConfigurationBuilder<TDb> where TDb : DbContext
  {
    private static readonly IConfigurationRoot configuration;

    static DbConfigurationBuilder()
    {
      var cb = new ConfigurationBuilder();
      cb.AddJsonFile("appsettings.json");
      cb.AddUserSecrets<TDb>();
      configuration = cb.Build();
    }
    public static string GetConnectionString(string name)
      => configuration.GetConnectionString(name) ?? name;

  }
}
