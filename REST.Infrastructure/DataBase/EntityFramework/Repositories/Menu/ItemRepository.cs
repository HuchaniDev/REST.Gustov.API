using Microsoft.EntityFrameworkCore;
using REST.Domain.Common.Enums;
using REST.Domain.Dtos.Menu;
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

  public async Task<bool> UpdatePriceAsync(int id, decimal price)
  {
    return  await context.Items
      .Where(i => i.Id == id)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.Price, price))>0;
  }

  public async Task<bool> UpdateStockAsync(int id, int stock)
  {
    return await context.Items
      .Where(i => i.Id == id)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.Stock, stock))>0;
  }

  public async Task<bool> UpdateStatusAsync(int id, StatusEnum status)
  {
    return await context.Items
      .Where(i => i.Id == id)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.Status, status))>0;
  }

  public async Task<bool> IsDuplicateNameAsync(string name, int id)
  {
    return await context.Items
      .AnyAsync(i => i.Name == name && i.Id != id);
  }

  public Task<CategoryItemsDto[]> GetAllAsync(string? searchTerm,int category, int status)
  {
    var query = context.Items.AsNoTracking().AsQueryable();
    if (searchTerm != null)
      query = query.Where(i => i.Name.Contains(searchTerm));
    
    if (category != 0)
      query = query.Where(i => i.CategoryId == category);

    if (status != 0)
    {
      var enumStatus = (StatusEnum)status;
      query = query.Where(i => i.Status == enumStatus);
    }
    
    var items = query
      .Select(i => new{
        categoryId=i.Category.Id,
        categoryName=i.Category.Name,
        categoryDesc=i.Category.Description,
        itemId=i.Id,
        itemName=i.Name,
        i.Price,
        i.Stock,
        statusColor=i.Status.GetColor(),
        status=i.Status.ToString(),
        i.ImageUrl
        })
      .GroupBy(i => i.categoryId)
      .Select(g=>new CategoryItemsDto(
        g.Key,
        g.First().categoryName,
        g.First().categoryDesc,
        g.Select(i=>new ItemsSummaryDto(
          i.itemId,
          i.itemName,
          i.Price,
          i.Stock,
          i.statusColor,
          i.status,
          i.ImageUrl
          )).ToArray()
        ))
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