namespace REST.Domain.Dtos.Menu;

public record CategoryItemsDto(
  int CategoryId,
  string CategoryName,
  string? CategoryDescription,
  ItemsSummaryDto[] Items
  );