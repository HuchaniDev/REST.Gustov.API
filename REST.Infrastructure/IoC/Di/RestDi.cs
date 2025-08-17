using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using REST.Infrastructure.DataBase.EntityFramework.Context;

namespace REST.Infrastructure.IoC.Di;

public static class RestDi
{
  public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
  {
    var connectionString = configuration["ConnectionStrings:remoteConnection"];
        
    collection.AddDbContext<RestDbContext>(options => { options.UseSqlServer(connectionString); }
    );

    collection.AddDbContextFactory<RestDbContext>(options =>
        options.UseSqlServer(connectionString),
      ServiceLifetime.Scoped);
    return collection;
  }
}