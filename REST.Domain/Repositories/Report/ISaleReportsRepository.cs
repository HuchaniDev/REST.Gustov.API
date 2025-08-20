using REST.Domain.Dtos.Reports;

namespace REST.Domain.Repositories.Report;

public interface ISaleReportsRepository
{
  Task<DailySalesReportDto?>GetReportByDate(DateOnly date);
}