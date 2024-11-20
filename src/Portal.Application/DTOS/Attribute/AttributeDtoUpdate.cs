namespace DeviceService.Application.DTOS
{
    public class AttributeDtoUpdate : AttributeDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}