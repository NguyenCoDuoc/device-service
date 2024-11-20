namespace DeviceService.Application.DTOS
{
    public class DepartmentDtoCreate : DepartmentDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}