using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("device_attribute")]
public class DeviceAttribute : CrudFieldEntity<long>
{
    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Column("attribute_id")]
    [Display(Name = "AttributeId")]
    public required long AttributeId { get; set; }

    [Column("device_id")]
    [Display(Name = "DeviceId")]
    public required long DeviceId { get; set; }

    [Column("attribute_value_id")]
    [Display(Name = "AttributeValueId")]
    public required long AttributeValueId { get; set; }
}