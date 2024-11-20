namespace DeviceService.Application.DTOS
{
    public class DeviceUnitDtoUpdate : DeviceUnitDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}