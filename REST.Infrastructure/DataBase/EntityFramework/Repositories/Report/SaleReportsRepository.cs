using Microsoft.EntityFrameworkCore;
using REST.Domain.Dtos.Reports;
using REST.Domain.Repositories.Report;
using REST.Infrastructure.DataBase.EntityFramework.Context;

namespace REST.Infrastructure.DataBase.EntityFramework.Repositories.Report;

public class SaleReportsRepository(RestDbContext context):ISaleReportsRepository
{
  public async Task<DailySalesReportDto?> GetReportByDate(DateOnly date)
  {
    var startDate = date.ToDateTime(TimeOnly.MinValue); // 2025-08-17 00:00:00
    var endDate = date.ToDateTime(TimeOnly.MaxValue);   // 2025-08-17 23:59:59

    var sales = await context.SaleDetails
      .AsNoTracking()
      .Where(s => s.Sale.Date >= startDate && s.Sale.Date <= endDate)
      .Select(s => new
      {
        s.Sale.Date,
        s.Sale.Total,
        s.ItemId,
        s.Item.Name,
        s.SubTotal,
        s.Quantity,
      }).ToListAsync();
    
    if (!sales.Any())
      return null;

    var itemsGrouped = sales
      .GroupBy(d => new { d.ItemId, d.Name, d.SubTotal })
      .Select(g => new ItemReportDto(
        g.Key.Name,
        g.Sum(x => x.Quantity),
        g.Sum(x => g.Key.SubTotal/x.Quantity),
        g.Key.SubTotal
      ))
      .ToList();

    return new DailySalesReportDto
    (
      date,
      itemsGrouped,
      itemsGrouped.Sum(i => i.SubTotal)
    );
  }
}