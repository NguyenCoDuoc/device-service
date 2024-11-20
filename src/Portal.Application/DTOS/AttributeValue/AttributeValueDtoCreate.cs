namespace DeviceService.Application.DTOS
{
    public class AttributeValueDtoCreate : AttributeValueDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}