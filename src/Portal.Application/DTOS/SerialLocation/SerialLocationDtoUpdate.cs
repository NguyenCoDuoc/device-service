namespace DeviceService.Application.DTOS
{
    public class SerialLocationDtoUpdate : SerialLocationDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}