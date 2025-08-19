using Microsoft.EntityFrameworkCore;
using REST.Application.Services.CodeGenerator;
using REST.Domain.Dtos.Sales;
using REST.Domain.Models.Sales;
using REST.Domain.Repositories.Sale;
using REST.Infrastructure.DataBase.EntityFramework.Context;
using REST.Infrastructure.DataBase.EntityFramework.Entities.Sale;

namespace REST.Infrastructure.DataBase.EntityFramework.Repositories.Sale;

public class SaleRepository(RestDbContext context):ISaleRepository
{
  public async Task<int> SaveSaleAsync(List<SaleDetailModel> items,decimal total, string saleCode)
  {
    await using var transaction = await context.Database.BeginTransactionAsync();
    try
    {
      // 1. Crear registro de venta
      var sale = new SaleEntity
      {
        Date = DateTime.UtcNow,
        Total = total,
        SaleCode =saleCode
      };

      context.Sales.Add(sale);
      await context.SaveChangesAsync();

      // 2. Crear detalles
      foreach (var item in items)
      {
        var detail = new SalesDetailEntity
        {
          SaleId = sale.Id,
          ItemId = item.ItemId,
          Quantity = item.Quantity,
          SubTotal = item.SubTotal
        };

        context.SaleDetails.Add(detail);
      }
      await context.SaveChangesAsync();

      // 3. Confirmar transacción
      await transaction.CommitAsync();
      return sale.Id;
    }
    catch (Exception)
    {
      // Si falla algo se hace rollback
      await transaction.RollbackAsync();
      return 0;
    }
  }

  public async Task<bool> DeleteSaleAsync(int saleId)
  {
    return await context.Sales
      .Where(i => i.Id == saleId)
      .ExecuteDeleteAsync() > 0;
  }

  public Task<SaleDetailDto?> GetSaleByIdAsync(int saleId)
  {
    var sale = context.Sales
      .AsNoTracking()
      .Where(s => s.Id == saleId)
      .Select(s => new SaleDetailDto(
        s.Id,
        s.SaleCode,
        s.Date,
        s.Total,
        s.SalesDetails.Select(i => new ItemLiteDto(
          i.Id,
          i.Item.Name,
          i.SubTotal,
          i.Quantity
        )).ToArray()
      ))
      .FirstOrDefaultAsync();
    return sale;
  }

  public Task<string?> GetLastOrderByDateAsync(DateTime date)
  {
    return context.Sales
      .Where(s => s.Date.Date == date.Date)
      .OrderByDescending(s => s.Id) 
      .Select(s=>s.SaleCode)
      .FirstOrDefaultAsync();
  }
}