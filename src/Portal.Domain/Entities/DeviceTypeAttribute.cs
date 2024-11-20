using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("device_type_attribute")]
public class DeviceTypeAttribute : CrudFieldEntity<long>
{
    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Column("attribute_id")]
    [Display(Name = "AttributeId")]
    public required long AttributeId { get; set; }

    [Column("device_type_id")]
    [Display(Name = "DeviceTypeId")]
    public required long DeviceTypeId { get; set; }

    [Column("attribute_value_id")]
    [Display(Name = "AttributeValueId")]
    public required long AttributeValueId { get; set; }

    [NotMapped]
    [Column("attribute_value")]
    [Display(Name = "AttributeValue")]
    public string AttributeValue { get; set; }

    [NotMapped]
    [Column("attribute_name")]
    [Display(Name = "AttributeName")]
    public string AttributeName { get; set; }
}