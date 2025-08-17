using REST.Domain.ValidatorServices;

namespace REST.Domain.Models.Menu;

public class CategoryModel:TraceModel
{
  public int Id { get; private set; }
  public string Name { get; private set; }  
  public string? Description { get; private set; }

  public CategoryModel(int id, string name, string? description)
  {
    if(!id.IdIsValid())
      AddError("Id inválido");
    
    if(string.IsNullOrEmpty(name))
      AddError("nombre de categoria requerido");
    
    Id = id;
    Name = name;
    Description = description;
  }
}