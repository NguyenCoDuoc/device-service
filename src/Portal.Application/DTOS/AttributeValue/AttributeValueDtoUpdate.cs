namespace DeviceService.Application.DTOS
{
    public class AttributeValueDtoUpdate : AttributeValueDto
    {
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}