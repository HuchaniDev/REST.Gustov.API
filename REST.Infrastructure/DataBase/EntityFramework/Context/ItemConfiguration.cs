using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST.Domain.Models.Menu;
using REST.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace REST.Infrastructure.DataBase.EntityFramework.Context;

public class ItemConfiguration:IEntityTypeConfiguration<ItemsEntity>
{
  public void Configure(EntityTypeBuilder<ItemsEntity> builder)
  {
    builder.Property(i => i.Discount)
      .HasDefaultValue(0);
    
    builder.HasOne(i=>i.Category)
      .WithMany(c=>c.Items)
      .HasForeignKey(i=>i.CategoryId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}