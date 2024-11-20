namespace DeviceService.Application.DTOS
{
    public class LocationDto
    {
        public string? Name { get; set; }
        public long? Level { get; set; }
        public long? ParentId { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }
        public  long? DepartmentId { get; set; }
        public string? Description { get; set; }

    }
}