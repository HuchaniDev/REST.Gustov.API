namespace REST.Domain.Common.Response;

public static class HttpStatusMessages
{
  private static readonly Dictionary<int, string> _messages = new Dictionary<int, string>
  {
    { 200, "Operación realizada con éxito." },
    { 201, "Recurso creado correctamente." },
    { 202, "Solicitud recibida y en proceso." },
    { 204, "Operación exitosa. No hay contenido para mostrar." },
    { 400, "Solicitud inválida. Verifique los datos enviados." },
    { 401, "No autorizado. Debe iniciar sesión primero." },
    { 403, "No tiene permisos para realizar esta acción." },
    { 404, "Recurso no encontrado." },
    { 405, "Método no permitido para este recurso." },
    { 409, "No se pudo completar la acción porque hay un conflicto con la información existente." },
    { 422, "Los datos enviados no son válidos. Revise los campos e intente nuevamente." },
    { 429, "Demasiadas solicitudes. Intente nuevamente más tarde." },
    { 500, "Ocurrió un error inesperado. Intente nuevamente más tarde." },
    { 502, "El servidor no pudo procesar la solicitud por un error en un servicio intermedio." },
    { 503, "El servicio no está disponible actualmente. Intente más tarde." },
    { 504, "Tiempo de espera agotado. El servidor no respondió a tiempo." }
  };

  public static string GetMessage(int statusCode)
  {
    return _messages.GetValueOrDefault(statusCode, "Código de estado desconocido.");
  }
}