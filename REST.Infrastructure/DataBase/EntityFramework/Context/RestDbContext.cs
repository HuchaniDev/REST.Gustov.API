using Microsoft.EntityFrameworkCore;

namespace REST.Infrastructure.DataBase.EntityFramework.Context;

public class RestDbContext: DbContext
{
  public RestDbContext(DbContextOptions<RestDbContext> options): base(options)
  {
    //  other config
    throw new NotImplementedException();
  }
}