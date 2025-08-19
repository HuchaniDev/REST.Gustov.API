namespace REST.Domain;

public interface ICodeGeneratorService
{
  Task<string> GenerateCodeAsync(DateTime date);
}