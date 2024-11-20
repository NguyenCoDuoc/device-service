using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("attribute")]
public class Attribute : CrudFieldEntity<long>
{
    [Column("code")]
    [Display(Name = "Code")]
    public required string Code { get; set; }
    
    [Column("name")] 
    [Display(Name = "Name")]
    public required string Name { get; set; }

    [Column("table_name")]
    [Display(Name = "TableName")]
    public string TableName { get; set; }

    [Column("column_name")]
    [Display(Name = "ColumnName")]
    public string ColumnName { get; set; }

    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }

}