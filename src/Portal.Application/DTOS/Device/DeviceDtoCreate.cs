namespace DeviceService.Application.DTOS
{
    public class DeviceDtoCreate : DeviceDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? DeletedBy { get; set; }
    }
}