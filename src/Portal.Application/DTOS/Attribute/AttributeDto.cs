namespace DeviceService.Application.DTOS
{
    public class AttributeDto : BaseDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? TableName { get; set; }
        public string? ColumnName { get; set; } 
        public string? Description { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }

    }
}