namespace DeviceService.Application.DTOS
{
    public class DeviceDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long? Quantity { get; set; }
        public string? Model { get; set; } 
        public string? Description { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }
        public long DeviceTypeId { get; set; }
        public string? DeviceTypeCode { get; set; }
        public long IsDeleted { get; set; }

        public int serials { get; set; }

    }

    public class DeviceAttributeDto
    {
        public string? Description { get; set; }

        public long AttributeId { get; set; }

        public required long DeviceId { get; set; }

        public required long AttributeValueId { get; set; }

        public string? AttributeName { get; set; }

        public string? AttributeValue { get; set; }
        public int? Id { get; set; }
    }

    /// <summary>
    /// Device  attribute create dto
    /// </summary>
    public class DeviceAttributeDtoCreate : DeviceAttributeDto
    {
    }
}