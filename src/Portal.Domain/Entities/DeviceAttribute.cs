using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("serial_attribute")]
public class SerialAttribute : CrudFieldEntity<long>
{
    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Column("attribute_id")]
    [Display(Name = "AttributeId")]
    public required long AttributeId { get; set; }

    [Column("serial_id")]
    [Display(Name = "SerialId")]
    public required long SerialId { get; set; }

    [Column("attribute_value_id")]
    [Display(Name = "AttributeValueId")]
    public required long AttributeValueId { get; set; }
}