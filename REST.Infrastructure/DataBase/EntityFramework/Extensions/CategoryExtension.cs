using REST.Domain.Models.Menu;
using REST.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace REST.Infrastructure.DataBase.EntityFramework.Extensions;

public static class CategoryExtension
{
  public static CategoryEntity ToEntity(this CategoryModel category)
  {
    return new CategoryEntity
    {
      Id = category.Id,
      Name = category.Name,
      Description = category.Description
    };
  }

  public static CategoryModel ToModel(this CategoryEntity entity)
  {
    return new CategoryModel(
      entity.Id,
      entity.Name,
      entity.Description
    );
  }
}