using REST.Application.Services.Reports;

namespace Rest.Api.EndPoints.Reports;

public static class SaleReportEndPoints
{
  internal static void MapSaleReportEndpoints(this WebApplication webApp)
  {
    webApp.MapGroup("/sale-report").WithTags("reports").MapSaleReport();
  }

  private static void MapSaleReport(this RouteGroupBuilder groupBuilder)
  {
    groupBuilder.MapGet("/by-date",
      (SaleReportService service, DateOnly? date) =>
        service.GetReportByDate(date).ToApiResult()
    );
  }
}