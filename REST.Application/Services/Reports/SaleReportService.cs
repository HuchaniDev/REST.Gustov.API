using System.Net;
using REST.Domain.Common.Response;
using REST.Domain.Dtos.Reports;
using REST.Domain.Repositories.Report;

namespace REST.Application.Services.Reports;

public class SaleReportService(ISaleReportsRepository reportsRepository)
{
  public async Task<Result<DailySalesReportDto>> GetReportByDate(DateOnly? date)
  {
    DateOnly entryDate = date?? DateOnly.FromDateTime(DateTime.UtcNow);
    
    var report = await reportsRepository.GetReportByDate(entryDate);
    return Result<DailySalesReportDto>.Success(report, HttpStatusCode.OK);
  }

}