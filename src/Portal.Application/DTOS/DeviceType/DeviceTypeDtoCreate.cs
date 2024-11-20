namespace DeviceService.Application.DTOS
{
    public class DeviceTypeDtoCreate : DeviceTypeDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}