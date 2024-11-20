using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("device_unit")]
public class DeviceUnit : CrudFieldEntity<long>
{
    [Column("code")]
    [Display(Name = "Code")]
    public required string Code { get; set; }
    
    [Column("name")] 
    [Display(Name = "Name")]
    public required string Name { get; set; }
    

    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }
}