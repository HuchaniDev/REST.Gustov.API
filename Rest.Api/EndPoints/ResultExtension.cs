using REST.Domain.Common.Response;

namespace Rest.Api.EndPoints;

public static class ResultExtension
{
  public static async Task<IResult> ToApiResult<T>(this Task<Result<T>> resultTask)
  {
    var result = await resultTask;
    return Results.Json(result, statusCode: (int)result.StatusCode);
  }
}