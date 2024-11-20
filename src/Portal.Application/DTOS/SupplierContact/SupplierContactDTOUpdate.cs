namespace DeviceService.Application.DTOS.SupplierContact
{
    public class SupplierContactDTOUpdate : SupplierContactDTO
	{
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
       
        public SupplierContactDTOUpdate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
