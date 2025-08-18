using System.Net;
using REST.Domain.Common.Response;
using REST.Domain.Models.Menu;
using REST.Domain.Repositories.Menu;

namespace REST.Application.Services.Menu;

public class CategoryService(ICategoryRepository categoryRepository)
{
  public async Task<Result<object>> SaveCategory(CategoryModel category)
  {
    if(category.HasErrors())
      return Result<object>.Failure(category.GetAllMessageErrors(),HttpStatusCode.BadRequest);

    if(await categoryRepository.IsDuplicateNameAsync(category.Name,category.Id))
      return Result<object>.Failure([$"La categoria con el nombre {category.Name}, Ya existe."],HttpStatusCode.BadRequest);
    
    await categoryRepository.SaveAsync(category);
    return Result<object>.Success(new {}, HttpStatusCode.OK);
  }

  public async Task<Result<object>> GetAll()
  {
    var categories = await categoryRepository.GetAllAsync();
    return Result<object>.Success(categories, HttpStatusCode.OK);
  }

  public async Task<Result<object>> GetByIdAsync(int id)
  {
    var category = await categoryRepository.GetByIdAsync(id);
    if(category is null)
      return Result<object>.Failure([],HttpStatusCode.NotFound);
    
    return Result<object>.Success(category, HttpStatusCode.OK);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    return await categoryRepository.DeleteAsync(id)
      ? Result<object>.Success(new{},HttpStatusCode.OK) 
      : Result<object>.Failure([],HttpStatusCode.InternalServerError); 
  }
}