namespace DeviceService.Application.DTOS
{
    public class DeviceTypeDtoUpdate : DeviceTypeDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}