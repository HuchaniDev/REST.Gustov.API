using REST.Domain.Models.Menu;

namespace REST.Domain.Repositories.Menu;

public interface IItemRepository
{
  Task<ItemModel> SaveAsync(ItemModel category);
  Task<bool>IsDuplicateNameAsync(string name,int id);
  Task<ItemModel[]> GetAllAsync();
  Task<ItemModel?>GetByIdAsync(int id);
  Task<bool>DeleteAsync(int id);
}