using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("serial_location")]
public class SerialLocation : CrudFieldEntity<long>
{

    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Column("start_time")]
    [Display(Name = "StartTime")]
    public DateTime StartTime { get; set; }

    [Column("end_time")]
    [Display(Name = "EndTime")]
    public DateTime EndTime { get; set; }

    [Column("serial_id")]
    [Display(Name = "SerialId")]
    public long SerialId { get; set; }

    [Column("location_id")]
    [Display(Name = "LocationId")]
    public long LocationId { get; set; }
}