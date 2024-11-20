namespace DeviceService.Application.DTOS
{
    public class AttributeDtoCreate : AttributeDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}