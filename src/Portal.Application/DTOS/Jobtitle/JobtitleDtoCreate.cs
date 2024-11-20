namespace DeviceService.Application.DTOS
{
    public class JobtitleDtoCreate : JobtitleDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}