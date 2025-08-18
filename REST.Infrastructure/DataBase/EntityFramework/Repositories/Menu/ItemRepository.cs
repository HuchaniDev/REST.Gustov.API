using Microsoft.EntityFrameworkCore;
using REST.Domain.Models.Menu;
using REST.Domain.Repositories.Menu;
using REST.Infrastructure.DataBase.EntityFramework.Context;
using REST.Infrastructure.DataBase.EntityFramework.Extensions;

namespace REST.Infrastructure.DataBase.EntityFramework.Repositories.Menu;

public class ItemRepository(RestDbContext context):IItemRepository
{
  public async Task<ItemModel> SaveAsync(ItemModel item)
  {
    var entity = item.ToEntity();
    if (item.Id == 0)
      await context.Items.AddAsync(entity);
    else
    {
      var existing = await context.Items.FindAsync(item.Id);
      if (existing is null)
        throw new KeyNotFoundException($"El Item con Id {item.Id} no existe.");
      context.Entry(existing).CurrentValues.SetValues(entity);
      entity = existing;
    }
    await context.SaveChangesAsync();
    return entity.ToModel();
  }

  public async Task<bool> IsDuplicateNameAsync(string name, int id)
  {
    return await context.Items
      .AnyAsync(i => i.Name == name && i.Id != id);
  }

  public Task<ItemModel[]> GetAllAsync()
  {
    var items = context.Items
      .AsNoTracking()
      .Select(i => i.ToModel())
      .ToArrayAsync();
    return items;
  }

  public async Task<ItemModel?> GetByIdAsync(int id)
  {
    var item = await context.Items
      .FindAsync(id);
    return item?.ToModel();
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await context.Items
      .Where(i => i.Id == id)
      .ExecuteDeleteAsync() > 0;
  }
}