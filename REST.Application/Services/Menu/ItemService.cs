using System.Net;
using REST.Domain.Common.Response;
using REST.Domain.Models.Menu;
using REST.Domain.Repositories.Menu;

namespace REST.Application.Services.Menu;

public class ItemService(IItemRepository itemRepository)
{
  public async Task<Result<object>> SaveItem(ItemModel item)
  {
    if(item.HasErrors())
      return Result<object>.Failure(item.GetAllMessageErrors(),HttpStatusCode.BadRequest);

    if(await itemRepository.IsDuplicateNameAsync(item.Name,item.Id))
      return Result<object>.Failure([$"El item con el nombre {item.Name}, Ya existe."],HttpStatusCode.BadRequest);
    
    await itemRepository.SaveAsync(item);
    return Result<object>.Success(new {}, HttpStatusCode.OK);
  }

  public async Task<Result<object>> GetAll()
  {
    var items = await itemRepository.GetAllAsync();
    return Result<object>.Success(items, HttpStatusCode.OK);
  }

  public async Task<Result<object>> GetByIdAsync(int id)
  {
    var item = await itemRepository.GetByIdAsync(id);
    if(item is null)
      return Result<object>.Failure([],HttpStatusCode.NotFound);
    
    return Result<object>.Success(item, HttpStatusCode.OK);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    return await itemRepository.DeleteAsync(id)
      ? Result<object>.Success(new{},HttpStatusCode.OK) 
      : Result<object>.Failure([],HttpStatusCode.InternalServerError); 
  }
}