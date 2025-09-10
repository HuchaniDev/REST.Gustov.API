using REST.Domain.Models.Menu;
using REST.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace REST.Infrastructure.DataBase.EntityFramework.Extensions;

public static class ItemsExtension
{
  public static ItemsEntity ToEntity(this ItemModel item)
  {
    return new ItemsEntity
    {
      Id = item.Id,
      Name = item.Name,
      Price = item.Price,
      Description = item.Description,
      Stock = item.Stock,
      Status = item.Status,
      Discount = item.Discount,
      ImageUrl = item.ImageUrl,
      CategoryId = item.CategoryId
    };
  }

  public static ItemModel ToModel(this ItemsEntity item)
  {
    return new ItemModel(
      item.Id,
      item.Name,
      item.Price,
      item.Description,
      item.Stock,
      item.Status,
      item.Discount,
      item.ImageUrl,
      item.CategoryId,
      true
    );
  }
}