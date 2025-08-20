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
    groupBuilder.MapPost("/update-price/{id:int}",
      (ItemService service, int id, decimal price)=>
        service.UpdatePrice(id,price).ToApiResult()
    );
    groupBuilder.MapPost("/update-stock/{id:int}",
      (ItemService service, int id, int stock)=>
        service.UpdateStock(id,stock).ToApiResult()
    );
    groupBuilder.MapPost("/update-status/{id:int}",
      (ItemService service, int id, bool status)=>
        service.UpdateStatus(id, status).ToApiResult()
    );
    groupBuilder.MapGet("/all",
      (ItemService service) =>
        service.GetAll().ToApiResult()
    );
    groupBuilder.MapGet("/all-filter",
      (ItemService service, string? searchTerm=null,int category=0, bool status=false) =>
        service.GetAllFilter(searchTerm,category, status).ToApiResult()
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