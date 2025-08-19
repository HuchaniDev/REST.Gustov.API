using REST.Domain;
using REST.Domain.Repositories.Sale;

namespace REST.Application.Services.CodeGenerator;

public class CodeGeneratorService(ISaleRepository saleRepository):ICodeGeneratorService
{
  public async Task<string> GenerateCodeAsync(DateTime date)
  {
    // Buscar el último código del día
    var lastOrder = await saleRepository.GetLastOrderByDateAsync(date.Date);

    int nextNumber = 1;

    if (lastOrder != null)
    {
      var lastCode = lastOrder; // Ej: "ORD-005"
      var numberPart = int.Parse(lastCode.Split('-')[1]);
      nextNumber = numberPart + 1;
    }

    return $"ORD-{nextNumber:D3}"; // ORD-001, ORD-002...
  }
}