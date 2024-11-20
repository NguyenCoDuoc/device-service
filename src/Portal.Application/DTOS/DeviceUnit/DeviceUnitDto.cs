namespace DeviceService.Application.DTOS
{
    public class DeviceUnitDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long? Quantity { get; set; }
        public long? Model { get; set; } 
        public string? Description { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }

    }
}