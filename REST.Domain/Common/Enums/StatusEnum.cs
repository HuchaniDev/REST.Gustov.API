namespace REST.Domain.Common.Enums;

public enum StatusEnum
{
  Disponible = 1,
  Agotado = 2,
  NoPreparado = 3
}

public static class StatusEnumExtensions
{
  public static string GetColor(this StatusEnum status)
  {
    return status switch
    {
      StatusEnum.Disponible => "#4CAF50", // Verde suave
      StatusEnum.Agotado => "#F44336",   // Rojo agradable
      StatusEnum.NoPreparado => "#FF9800", // Naranja cálido
      _ => "#9E9E9E" // Gris neutro (default)
    };
  }
}