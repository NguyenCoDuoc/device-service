namespace DeviceService.Application.DTOS
{
    public class SerialDto : BaseDto
    {
        public string? Code { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? PurchaseDate { get; set; } 
        public string? Description { get; set; }
        public string? WarrantyPeriod { get; set; }
        public string? status { get; set; }
        public long? DeviceId { get; set; }
        public long? LocationId { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }
        public string? SerialCode { get; set; }

    }

    public class SerialAttributeDto
    {
        public string? Description { get; set; }

        public long AttributeId { get; set; }

        public required long SerialId { get; set; }

        public required long AttributeValueId { get; set; }

        public string? AttributeName { get; set; }

        public string? AttributeValue { get; set; }
        public int? Id { get; set; }

        public long? CreatedBy { get; set; }
    }

    /// <summary>
    /// Serial  attribute create dto
    /// </summary>
    public class SerialAttributeDtoCreate : SerialAttributeDto
    {
    }
}