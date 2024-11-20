namespace DeviceService.Application.DTOS
{
    public class SerialDtoUpdate : SerialDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}