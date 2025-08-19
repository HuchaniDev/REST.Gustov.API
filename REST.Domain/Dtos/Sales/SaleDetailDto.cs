namespace REST.Domain.Dtos.Sales;

public record SaleDetailDto(
  int SaleId,
  string SaleCode,
  DateTime SaleDate,
  decimal Total,
  ItemLiteDto[] Items
  );