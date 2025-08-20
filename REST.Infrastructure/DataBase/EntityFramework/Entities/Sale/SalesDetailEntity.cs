using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REST.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace REST.Infrastructure.DataBase.EntityFramework.Entities.Sale;

[Table("SalesDetail", Schema = "MENU")]
public class SalesDetailEntity:BaseAuditable,IIdentifiable
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("id")]
  public int Id { get; set; }
  
  public int SaleId { get; set; }
  public int ItemId { get; set; }
  [Required]
  public int Quantity { get; set; }
  
  [Column(TypeName = "decimal(18,2)")] 
  public decimal SubTotal { get; set; }
  
  //relations
  public virtual SaleEntity Sale { get; set; }
  public virtual ItemsEntity Item { get; set; }
}