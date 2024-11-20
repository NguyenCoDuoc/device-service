namespace DeviceService.Application.DTOS
{
    public class SerialDto : BaseDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? PurchaseDate { get; set; } 
        public string? Description { get; set; }
        public string? WarrantyPeriod { get; set; }
        public string? status { get; set; }
        public long? DeviceId { get; set; }
        public long? LocationId { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }

    }
}