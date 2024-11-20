namespace DeviceService.Application.DTOS
{
    public class DeviceUnitDtoCreate : DeviceUnitDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}