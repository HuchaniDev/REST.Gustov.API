using REST.Application.Services.Menu;
using REST.Domain.Models.Menu;

namespace Rest.Api.EndPoints.Menu;

public static class CategoryEndPoints
{
  internal static void MapCategoryEndpoints(this WebApplication webApp)
  {
    webApp.MapGroup("/category").WithTags("category").MapCategory();
  }

  private static void MapCategory(this RouteGroupBuilder groupBuilder)
  {
    groupBuilder.MapPost("/",
      (CategoryService service, CategoryModel category)=>
        service.SaveCategory(category).ToApiResult()
    );
    groupBuilder.MapGet("/all",
      (CategoryService service) =>
        service.GetAll().ToApiResult()
    );
    groupBuilder.MapGet("/by-id/{id:int}",
      (CategoryService service, int id) =>
        service.GetByIdAsync(id).ToApiResult()
    );
    groupBuilder.MapDelete("/{id:int}",
      (CategoryService service, int id) =>
        service.DeleteAsync(id).ToApiResult()
    );
  }
  
}