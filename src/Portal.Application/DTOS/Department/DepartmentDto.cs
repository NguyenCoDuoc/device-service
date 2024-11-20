namespace DeviceService.Application.DTOS
{
    public class DepartmentDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long? Level { get; set; }
        public long? ParentId { get; set; }
        public string? TenantId { get; set; }
        public long? Id { get; set; }

    }
}