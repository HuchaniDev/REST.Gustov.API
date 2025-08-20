namespace REST.Domain.Models;

public class TraceModel
{
  public bool HasErrors() => Errors.Count > 0;
  private IList<string> Errors { get; set; } = [];

  protected void AddError(string message)
  {
    Errors.Add(message);
  }

  public IList<string> GetAllErrors()
  {
    return Errors;
  }

  public List<string> GetAllMessageErrors()
  {
    return Errors.ToList();
  }
}