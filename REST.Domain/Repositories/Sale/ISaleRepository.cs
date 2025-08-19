using REST.Domain.Dtos.Sales;
using REST.Domain.Models.Sales;

namespace REST.Domain.Repositories.Sale;

public interface ISaleRepository
{
  Task<int>SaveSaleAsync(List<SaleDetailModel> items,decimal total, string saleCode);
  Task<bool>DeleteSaleAsync(int saleId);
  Task<SaleDetailDto?>GetSaleByIdAsync(int saleId);
  Task<string?>GetLastOrderByDateAsync(DateTime date);
}