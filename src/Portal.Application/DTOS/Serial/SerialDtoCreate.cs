namespace DeviceService.Application.DTOS
{
    public class SerialDtoCreate : SerialDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public string? DeviceCode { get; set; }
        public string? SerialCode { get; set; }
        public long? Serials { get; set; }
    }
}