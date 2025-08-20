using REST.Application.Services.Sale;
using REST.Domain.Models.Sales;

namespace Rest.Api.EndPoints.Sale;

public static class SaleEndPoints
{
  internal static void MapSaleEndpoints(this WebApplication webApp)
  {
    webApp.MapGroup("/sale").WithTags("sale").MapSales();
  }

  private static void MapSales(this RouteGroupBuilder groupBuilder)
  {
    groupBuilder.MapPost("/",
      (SaleService service, List<SaleDetailModel> items) =>
        service.SaveSale(items).ToApiResult()
    );
    groupBuilder.MapGet("/sale-by-id/{saleId:int}",
      (SaleService service, int saleId) =>
        service.GetSaleByIdAsync(saleId).ToApiResult()
    );
  }
}