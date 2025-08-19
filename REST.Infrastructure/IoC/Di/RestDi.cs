using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using REST.Application.Services.CodeGenerator;
using REST.Application.Services.Menu;
using REST.Application.Services.Reports;
using REST.Application.Services.Sale;
using REST.Domain;
using REST.Domain.Repositories.Menu;
using REST.Domain.Repositories.Sale;
using REST.Infrastructure.DataBase.EntityFramework.Context;
using REST.Infrastructure.DataBase.EntityFramework.Repositories.Menu;
using REST.Infrastructure.DataBase.EntityFramework.Repositories.Sale;

namespace REST.Infrastructure.IoC.Di;

public static class RestDi
{
  public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
  {
    var connectionString = configuration["ConnectionStrings:localConnection"];
        
    collection.AddDbContext<RestDbContext>(options => { options.UseSqlServer(connectionString); }
    );

    collection.AddDbContextFactory<RestDbContext>(options =>
        options.UseSqlServer(connectionString),
      ServiceLifetime.Scoped);
    return collection;
  }
  
  public static IServiceCollection RegisterServices(this IServiceCollection collection)
  {
    // collection.AddTransient<UserService>();
    // collection.AddScoped<AuthService>();

    collection.AddTransient<CategoryService>();
    collection.AddTransient<ItemService>();
    collection.AddTransient<SaleService>();
    collection.AddScoped<ICodeGeneratorService, CodeGeneratorService>();
    collection.AddTransient<SaleReportService>();
    return collection;
  }
    
  public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
  {
    // collection.AddTransient<IUserRepository, UserRepository>();
    collection.AddTransient<ICategoryRepository, CategoryRepository>();
    collection.AddTransient<IItemRepository, ItemRepository>();
    collection.AddTransient<ISaleRepository, SaleRepository>();
    collection.AddTransient<ISaleRepository, SaleRepository>();
    return collection;
  }
}