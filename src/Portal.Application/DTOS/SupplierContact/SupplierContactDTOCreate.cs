namespace DeviceService.Application.DTOS.SupplierContact
{
    public class SupplierContactDTOCreate : SupplierContactDTO
	{
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
       
        public SupplierContactDTOCreate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
