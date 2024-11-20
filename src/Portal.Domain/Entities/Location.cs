using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("location")]
public class Location : CrudFieldEntity<long>
{
    [Column("name")] 
    [Display(Name = "Name")]
    public required string Name { get; set; }

    [Column("description")]
    [Display(Name = "Description")]
    public required string Description { get; set; }

    [Column("level")] 
    [Display(Name = "Level")]
    public  long Level { get; set; }

    [Column("parent_id")]
    [Display(Name = "ParentId")]
    public  long ParentId { get; set; }

    [Column("department_id")]
    [Display(Name = "DpartmentId")]
    public long DpartmentId { get; set; }
}