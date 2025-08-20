using System.Net;
using REST.Domain;
using REST.Domain.Common.Response;
using REST.Domain.Dtos.Sales;
using REST.Domain.Models.Sales;
using REST.Domain.Repositories.Sale;

namespace REST.Application.Services.Sale;

public class SaleService(ISaleRepository saleRepository,
  ICodeGeneratorService codeGenerator)
{
  public async Task<Result<SaleDetailDto?>> SaveSale(List<SaleDetailModel> items)
  {

    var errors = new List<string>();
    decimal total = items.Select(i=>i.GetSubTotal())
      .Sum();

    foreach (var item in items)
      if(item.HasErrors())
        errors.AddRange(item.GetAllMessageErrors());
    
    if (errors.Any())
      return Result<SaleDetailDto?>.Failure(errors,HttpStatusCode.BadRequest);
    
    var code = await codeGenerator.GenerateCodeAsync(DateTime.UtcNow);

    int saleId = await saleRepository.SaveSaleAsync(items, total, code);
    if (saleId>0)
    {
      var sale = await saleRepository.GetSaleByIdAsync(saleId);
      return Result<SaleDetailDto?>.Success(sale, HttpStatusCode.OK);
    }
    return Result<SaleDetailDto?>.Failure(["No se pudo guardar la orden"],HttpStatusCode.InternalServerError);
  }

  public async Task<Result<SaleDetailDto?>> GetSaleByIdAsync(int saleId)
  {
    if (saleId>0)
    {
      var sale = await saleRepository.GetSaleByIdAsync(saleId);
      return Result<SaleDetailDto?>.Success(sale, HttpStatusCode.OK);
    }
    return Result<SaleDetailDto?>.Failure(["La orden no existe"],HttpStatusCode.NotFound);
  }
}