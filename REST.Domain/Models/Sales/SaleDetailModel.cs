using System.Text.Json.Serialization;
using REST.Domain.ValidatorServices;

namespace REST.Domain.Models.Sales;

public class SaleDetailModel:TraceModel
{
  public int ItemId{get; private set;}
  public int Quantity{get; private set;}
  public decimal SubTotal{ get; private set;}
  
  [JsonConstructor]
  public SaleDetailModel(int itemId, int quantity, decimal subTotal)
  {
    if (!itemId.ForeignKeyIsValid())
      AddError("Item Inválido");
    
    if(quantity<1)
      AddError("ingrese cantidad mayor o igual a 1");

    ItemId = itemId;
    Quantity = quantity;
    SubTotal = subTotal;
  }
  
  public decimal GetSubTotal()=>SubTotal;
}