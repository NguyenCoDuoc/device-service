namespace DeviceService.Application.DTOS
{
    public class AttributeValueDto : BaseDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? DataType { get; set; } 
        public string? Description { get; set; }
        public long? Id { get; set; }

    }
}