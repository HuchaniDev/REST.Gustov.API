using REST.Domain.Common.Enums;
using REST.Domain.Dtos.Menu;
using REST.Domain.Models.Menu;

namespace REST.Domain.Repositories.Menu;

public interface IItemRepository
{
  Task<ItemModel> SaveAsync(ItemModel category);
  Task<bool>UpdatePriceAsync(int id, decimal price);
  Task<bool>UpdateStockAsync(int id, int stock);
  Task<bool>UpdateStatusAsync(int id, bool status);
  Task<bool>IsDuplicateNameAsync(string name,int id);
  public Task<CategoryItemsDto[]> GetAllAsync();
  Task<CategoryItemsDto[]> GetAllFilterAsync(string? searchTerm,int category,bool status);
  Task<ItemModel?>GetByIdAsync(int id);
  Task<bool>DeleteAsync(int id);
}