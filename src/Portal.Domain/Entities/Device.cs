using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("device")]
public class Device : CrudFieldEntity<long>
{
    [Column("code")]
    [Display(Name = "Code")]
    public required string Code { get; set; }
    
    [Column("name")] 
    [Display(Name = "Name")]
    public required string Name { get; set; }

    [Column("model")]
    [Display(Name = "Model")]
    public string Model { get; set; }

    [Column("quantity")]
    [Display(Name = "Quantity")]
    public long Quantity { get; set; }

    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }


    [Column("device_type_id")]
    [Display(Name = "DeviceTypeId")]
    public long DeviceTypeId { get; set; }
}