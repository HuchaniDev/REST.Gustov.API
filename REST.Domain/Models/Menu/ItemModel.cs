using System.Text.Json.Serialization;
using REST.Domain.Common.Enums;
using REST.Domain.ValidatorServices;

namespace REST.Domain.Models.Menu;

public class ItemModel:TraceModel
{
  public int Id { get; private set; }
  public string Name { get; private set; }
  public decimal Price { get; private set; }
  public string? Description { get; private set; }
  public int Stock { get; private set; }
  public bool Status { get; private set; }
  public string ImageUrl { get; private set; }
  public int CategoryId { get; private set; }

  [JsonConstructor]
  public ItemModel(int id, string name, decimal price, string? description, int stock, string imageUrl, int categoryId)
  {
    if(!id.IdIsValid())
      AddError("Id Inválido");
    
    if(string.IsNullOrEmpty(name))
      AddError("Nombre es requerido");
    
    if(price < 0)
      AddError("El precio no pude ser negativo");
    
    if(stock<0)
      AddError("el stockno puede ser negativo");
    
    if(!categoryId.ForeignKeyIsValid())
      AddError("Categoria inválido");
    
    Id = id;
    Name = name;
    Price = price;
    Description = description;
    Stock = stock;
    Status = true;
    ImageUrl = imageUrl;
    CategoryId = categoryId;
  
  }
  public ItemModel(int id, string name, decimal price, string? description, int stock,bool status, string imageUrl, int categoryId,bool skipValidations)
  {
    Id = id;
    Name = name;
    Price = price;
    Description = description;
    Stock = stock;
    Status = status;
    ImageUrl = imageUrl;
    CategoryId = categoryId;
  }
}
