using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REST.Domain.Common.Enums;

namespace REST.Infrastructure.DataBase.EntityFramework.Entities.Menu;

[Table("Item", Schema = "MENU")]
public class ItemsEntity:BaseAuditable,IIdentifiable
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("id")]
  public int Id { get; set; }
  
  [MaxLength(80)]
  public string Name { get; set; }
  
  [Column(TypeName = "decimal(18,2)")] 
  public decimal Price { get; set; }
  
  [MaxLength(400)]
  public string? Description { get; set; }
  
  [Required]
  public int Stock { get; set; }
  
  public StatusEnum Status { get; set; }
  
  public string ImageUrl { get; set; }
  public int CategoryId { get; set; }
  
  //relations
  public virtual CategoryEntity Category { get; set; }
}