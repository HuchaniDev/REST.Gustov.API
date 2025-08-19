using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST.Infrastructure.DataBase.EntityFramework.Entities.Sale;

namespace REST.Infrastructure.DataBase.EntityFramework.Context;

public class SalesDetailConfiguration:IEntityTypeConfiguration<SalesDetailEntity>
{
  public void Configure(EntityTypeBuilder<SalesDetailEntity> builder)
  {
    builder.HasOne(sd=>sd.Sale)
      .WithMany(sd=>sd.SalesDetails)
      .HasForeignKey(sd=>sd.SaleId)
      .OnDelete(DeleteBehavior.Cascade);
    
    builder.HasOne(sd=>sd.Item)
      .WithMany(sd=>sd.SalesDetails)
      .HasForeignKey(sd=>sd.ItemId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}