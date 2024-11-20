namespace DeviceService.Application.DTOS
{
    public class LocationDTOUpdate : LocationDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}