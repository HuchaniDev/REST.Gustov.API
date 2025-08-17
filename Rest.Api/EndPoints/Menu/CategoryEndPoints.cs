using REST.Application.Services.Menu;
using REST.Domain.Common.Response;
using REST.Domain.Models.Menu;

namespace Rest.Api.EndPoints;

public static class CategoryEndPoints
{
  internal static void MapCategoryEndpoints(this WebApplication webApp)
  {
    webApp.MapGroup("/category").WithTags("category").MapCategory();
  }

  private static void MapCategory(this RouteGroupBuilder groupBuilder)
  {
    groupBuilder.MapPost(
      "/",
      (CategoryService service, CategoryModel category)=>
        service.SaveCategory(category).ToApiResult()
    );
  }
  
}