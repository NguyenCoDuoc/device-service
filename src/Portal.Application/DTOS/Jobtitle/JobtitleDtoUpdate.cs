namespace DeviceService.Application.DTOS
{
    public class JobtitleDtoUpdate : JobtitleDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}