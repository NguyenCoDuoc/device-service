using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("attribute_value")]
public class AttributeValue : CrudFieldEntity<long>
{
    
    [Column("data_type")]
    [Display(Name = "DataType")]
    public string DataType { get; set; }

    [Column("value")]
    [Display(Name = "Value")]
    public string Value { get; set; }

    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }
}