using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace REST.Infrastructure.DataBase.EntityFramework.Context;

public class RestDbContextFactory:IDesignTimeDbContextFactory<RestDbContext>
{
  public RestDbContext CreateDbContext(string[] args)
  {
    IConfigurationRoot configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory()) 
      .AddUserSecrets<RestDbContextFactory>()  
      .Build();
        
    string? connectionString = configuration["ConnectionStrings:localConnection"];
        
    if (string.IsNullOrEmpty(connectionString))
    {
      throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula ni estar vacía.");
    }
        
    var optionsBuilder = new DbContextOptionsBuilder<RestDbContext>();
    optionsBuilder.UseSqlServer(connectionString);
    return new RestDbContext(optionsBuilder.Options);
  }
}