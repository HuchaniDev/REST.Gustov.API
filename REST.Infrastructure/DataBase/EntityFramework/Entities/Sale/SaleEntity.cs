using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST.Infrastructure.DataBase.EntityFramework.Entities.Sale;

[Table("Sale", Schema = "MENU")]
public class SaleEntity:BaseAuditable,IIdentifiable
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("id")]
  public int Id { get; set; }
  
  public DateTime Date { get; set; }
  
  [Column(TypeName = "decimal(18,2)")] 
  public decimal Total { get; set; }
  
  [Required, MaxLength(20)]
  public string SaleCode { get; set; }
  
  //relations
  public virtual ICollection<SalesDetailEntity> SalesDetails { get; set; }
}