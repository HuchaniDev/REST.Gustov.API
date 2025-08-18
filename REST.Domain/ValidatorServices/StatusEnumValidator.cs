using REST.Domain.Common.Enums;

namespace REST.Domain.ValidatorServices;

public static class StatusEnumValidator
{
  public static bool IsValidStatus(this int estado)
  {
    return Enum.IsDefined(typeof(StatusEnum), estado);
  }
}