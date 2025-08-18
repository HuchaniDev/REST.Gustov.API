using REST.Application.Services.Menu;
using REST.Domain.Models.Menu;

namespace Rest.Api.EndPoints.Menu;

public static class ItemEndPoints
{
  internal static void MapItemEndpoints(this WebApplication webApp)
  {
    webApp.MapGroup("/item").WithTags("item").MapItem();
  }

  private static void MapItem(this RouteGroupBuilder groupBuilder)
  {
    groupBuilder.MapPost("/",
      (ItemService service, ItemModel item)=>
        service.SaveItem(item).ToApiResult()
    );
    groupBuilder.MapGet("/all",
      (ItemService service) =>
        service.GetAll().ToApiResult()
    );
    groupBuilder.MapGet("/by-id/{id:int}",
      (ItemService service, int id) =>
        service.GetByIdAsync(id).ToApiResult()
    );
    groupBuilder.MapDelete("/{id:int}",
      (ItemService service, int id) =>
        service.DeleteAsync(id).ToApiResult()
    );
  }
  
}