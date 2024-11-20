namespace DeviceService.Application.DTOS
{
    public class SerialDtoCreate : SerialDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}