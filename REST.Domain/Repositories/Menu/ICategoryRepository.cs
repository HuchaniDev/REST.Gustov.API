using REST.Domain.Models.Menu;

namespace REST.Domain.Repositories.Menu;

public interface ICategoryRepository
{
  Task<CategoryModel> SaveAsync(CategoryModel category);
  Task<bool>IsDuplicateNameAsync(string name,int id);
  Task<CategoryModel[]> GetAllAsync();
  Task<CategoryModel?>GetByIdAsync(int id);
  Task<bool>DeleteAsync(int id);
}