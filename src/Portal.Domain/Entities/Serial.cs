using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities;


[Table("serial")]
public class Serial : CrudFieldEntity<long>
{

    [Column("serial_number")]
    [Display(Name = "SerialNumber")]
    public string SerialNumber { get; set; }

    [Column("purchase_date")]
    [Display(Name = "purchase_date")]
    public DateTime PurchaseDate { get; set; }

    [Column("warranty_period")]
    [Display(Name = "WarrantyPeriod")]
    public string WarrantyPeriod { get; set; }

    [Column("device_id")]
    [Display(Name = "DeviceId")]
    public long DeviceId { get; set; }

    [Column("location_id")]
    [Display(Name = "LocationId")]
    public long LocationId { get; set; }

    [Column("status")]
    [Display(Name = "Status")]
    public long Status { get; set; }

    [Column("description")]
    [Display(Name = "Description")]
    public string Description { get; set; }

}