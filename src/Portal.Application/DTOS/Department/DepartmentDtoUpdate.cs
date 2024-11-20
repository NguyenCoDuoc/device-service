namespace DeviceService.Application.DTOS
{
    public class DepartmentDtoUpdate : DepartmentDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}