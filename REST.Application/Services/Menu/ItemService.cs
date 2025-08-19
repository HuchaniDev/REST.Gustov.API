using System.Net;
using REST.Domain.Common.Enums;
using REST.Domain.Common.Response;
using REST.Domain.Models.Menu;
using REST.Domain.Repositories.Menu;
using REST.Domain.ValidatorServices;

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

  public async Task<Result<object>>UpdatePrice(int id, decimal price)
  {
    return await itemRepository.UpdatePriceAsync(id, price)
      ? Result<object>.Success(new { }, HttpStatusCode.OK)
      : Result<object>.Failure(["error al actualizar el precio"], HttpStatusCode.BadRequest);
  }
  public async Task<Result<object>>UpdateStock(int id, int stock)
  {
    return await itemRepository.UpdateStockAsync(id, stock)
      ? Result<object>.Success(new { }, HttpStatusCode.OK)
      : Result<object>.Failure(["error al actualizar el stock"], HttpStatusCode.BadRequest);
  }
  public async Task<Result<object>>UpdateStatus(int id, int status)
  {
    if(!status.IsValidStatus())
      return Result<object>.Failure(["Estado no disponible"],HttpStatusCode.BadRequest);

    return await itemRepository.UpdateStatusAsync(id,(StatusEnum)status)
      ? Result<object>.Success(new { }, HttpStatusCode.OK)
      : Result<object>.Failure(["error al actualizar el estado"], HttpStatusCode.BadRequest);
  }
  public async Task<Result<object>> GetAllFilter(string? searchTerm,int category,int status)
  {
    if(!(status.IsValidStatus() || status==0))
      return Result<object>.Failure(["Verifique el estado"], HttpStatusCode.BadRequest);
      
    var items = await itemRepository.GetAllAsync(searchTerm, category,status);
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