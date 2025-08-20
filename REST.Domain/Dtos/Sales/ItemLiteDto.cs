namespace REST.Domain.Dtos.Sales;

public record ItemLiteDto(
  int Id,
  string Name,
  decimal SubTotal,
  int Quantity
  );