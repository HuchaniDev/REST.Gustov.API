namespace REST.Domain.Dtos.Reports;

public record DailySalesReportDto(
  DateOnly Date,
  List<ItemReportDto> Items,
  decimal Total
  );

public record ItemReportDto
(
  string ItemName,
  int Quantity,
  decimal UnitPrice,
  decimal SubTotal
);