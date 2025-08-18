using Microsoft.EntityFrameworkCore;
using REST.Domain.Models.Menu;
using REST.Domain.Repositories.Menu;
using REST.Infrastructure.DataBase.EntityFramework.Context;
using REST.Infrastructure.DataBase.EntityFramework.Extensions;

namespace REST.Infrastructure.DataBase.EntityFramework.Repositories.Menu;

public class CategoryRepository(RestDbContext context):ICategoryRepository
{
  public async Task<CategoryModel> SaveAsync(CategoryModel category)
  {
    var entity = category.ToEntity();
    // Nuevo
    if (category.Id == 0) 
      await context.Categories.AddAsync(entity);
    // Existente
    else 
    {
      var existing = await context.Categories.FindAsync(category.Id);
      if (existing is null)
        throw new KeyNotFoundException($"La categoría con Id {category.Id} no existe.");
      // Actualizar
      context.Entry(existing).CurrentValues.SetValues(entity);
      entity = existing;
    }
    
    await context.SaveChangesAsync();
    return entity.ToModel();
  }

  public async Task<bool> IsDuplicateNameAsync(string name, int id)
  {
    return await context.Categories
      .AnyAsync(c => c.Name == name && c.Id != id);
  }

  public Task<CategoryModel[]> GetAllAsync()
  {
    var categories = context.Categories
      .AsNoTracking()
      .Select(c => c.ToModel())
      .ToArrayAsync();
    return categories;
  }

  public async Task<CategoryModel?> GetByIdAsync(int id)
  {
    var category = await context.Categories
      .FindAsync(id);
    return category?.ToModel();
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await context.Categories
      .Where(e => e.Id == id)
      .ExecuteDeleteAsync() > 0;
  }
}