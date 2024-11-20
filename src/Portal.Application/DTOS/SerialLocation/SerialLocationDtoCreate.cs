namespace DeviceService.Application.DTOS
{
    public class SerialLocationDtoCreate : SerialLocationDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}