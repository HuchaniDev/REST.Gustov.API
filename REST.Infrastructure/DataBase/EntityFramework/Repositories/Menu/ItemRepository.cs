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

  public async Task<bool> UpdateStatusAsync(int id, bool status)
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

  public Task<CategoryItemsDto[]> GetAllFilterAsync(string? searchTerm,int category, bool status)
  {
    var query = context.Items.AsNoTracking().AsQueryable();
    if (searchTerm != null)
      query = query.Where(i => i.Name.Contains(searchTerm));
    
    if (category != 0)
      query = query.Where(i => i.CategoryId == category);

    if (status)
      query = query.Where(i => i.Status);
    
    var items = query
      .OrderByDescending(i=>i.Status)
      .ThenByDescending(i=>i.Name)
      .Select(i => new{
        categoryId=i.Category.Id,
        categoryName=i.Category.Name,
        categoryDesc=i.Category.Description,
        itemId=i.Id,
        itemName=i.Name,
        i.Price,
        i.Stock,
        statusColor=i.Status?"#4CAF50":"#FF9800",
        status=i.Status,
        discount = i.Discount,
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
          i.discount,
          i.status,
          i.ImageUrl
          )).ToArray()
        ))
      .ToArrayAsync();
    return items;
  }
  
  public Task<CategoryItemsDto[]> GetAllAsync()
  {
    var items = context.Categories.AsNoTracking()
      .OrderByDescending(c=>c.Name)
      .Select(c => new CategoryItemsDto(
          c.Id,
          c.Name,
          c.Description,
          c.Items.Select(i=> new ItemsSummaryDto(
            i.Id,
            i.Name,
            i.Price,
            i.Stock,
            i.Status?"#4CAF50":"#FF9800",
            i.Discount,
            i.Status,
            i.ImageUrl
            ))
            .ToArray()
          )
        ).ToArrayAsync();
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