using Microsoft.EntityFrameworkCore;
using REST.Infrastructure.DataBase.EntityFramework.Entities;
using REST.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace REST.Infrastructure.DataBase.EntityFramework.Context;

public class RestDbContext: DbContext
{
  public DbSet<CategoryEntity>Categories { get; set; }
  public DbSet<ItemsEntity>Items { get; set; }
  
  public RestDbContext(DbContextOptions<RestDbContext> options): base(options)
  {
    // Configurado como NoTracking por defecto para optimizar consultas de solo lectura
    //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
  }
  
  public override int SaveChanges()
  {
    UpdateAuditFields();
    return base.SaveChanges();
  }
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    UpdateAuditFields();
    return base.SaveChangesAsync(cancellationToken);
  }
  
  private void UpdateAuditFields()
  {
    foreach (var entry in ChangeTracker.Entries<BaseAuditable>())
    {
      switch (entry.State)
      {
        case EntityState.Added:
          entry.Entity.CreatedAt = DateTime.UtcNow;
          entry.Entity.CreatedBy = GetCurrentUserId();
          entry.Entity.LastModifiedByAt = DateTime.UtcNow;
          entry.Entity.LastModifiedBy= GetCurrentUserId();
          break;

        case EntityState.Modified:
          entry.Property(nameof(BaseAuditable.CreatedAt)).IsModified = false;
          entry.Property(nameof(BaseAuditable.CreatedBy)).IsModified = false;
          entry.Entity.LastModifiedByAt = DateTime.UtcNow;
          entry.Entity.LastModifiedBy = 104;
          break;
      }
    }
  }
    
  private int GetCurrentUserId()
  {
    return 123;
  }
}