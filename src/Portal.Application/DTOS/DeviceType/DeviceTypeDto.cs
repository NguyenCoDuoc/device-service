namespace DeviceService.Application.DTOS
{
    public class DeviceTypeDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long? Quantity { get; set; }
        public long? Model { get; set; } 
        public string? Description { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }
        public long ParentId { get; set; }
        public long Level { get; set; }
        public  List<DeviceTypeAttributeDto>? Attributes { get; set; }
    }

    /// <summary>
    /// Device type attribute dto
    /// </summary>
    public class DeviceTypeAttributeDto
    {
        public string? Description { get; set; }

        public long AttributeId { get; set; }
    
        public required long DeviceTypeId { get; set; }

        public required long AttributeValueId { get; set; }

        public string? AttributeName { get; set; }

        public string? AttributeValue { get; set; }
        public int? Id { get; set; }
    }

    /// <summary>
    /// Device type attribute create dto
    /// </summary>
    public class DeviceTypeAttributeDtoCreate : DeviceTypeAttributeDto
    {
    }
}