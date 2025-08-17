using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST.Infrastructure.DataBase.EntityFramework.Entities.Menu;
[Table("Category",Schema = "MENU")]
public class CategoryEntity:BaseAuditable,IIdentifiable
{
  [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
  public int Id { get; set; }
  
  [MaxLength(50)]
  public string Name { get; set; }
  
  [MaxLength(200)]
  public string? Description { get; set; }
 
  //relations
  public virtual ICollection<ItemsEntity> Items { get; set; }=new HashSet<ItemsEntity>();
}