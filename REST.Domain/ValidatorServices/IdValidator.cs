namespace REST.Domain.ValidatorServices;

public static class IdValidator
{
  public static bool IdIsValid(this int id) => id >= 0;
  public static bool ForeignKeyIsValid(this int id) => id > 0;
}