namespace DeviceService.Application.DTOS
{
    public class DeviceDtoUpdate : DeviceDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}