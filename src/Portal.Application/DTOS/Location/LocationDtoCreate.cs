namespace DeviceService.Application.DTOS
{
    public class LocationDtoCreate : LocationDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}