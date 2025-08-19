namespace REST.Domain.Dtos.Menu;

public record ItemsSummaryDto(
  int Id,
  string Name,
  decimal Price,
  int Stock,
  string StatusColor,
  string Status,
  string ImageUrl
  );