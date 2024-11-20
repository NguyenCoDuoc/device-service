namespace DeviceService.Application.DTOS.SupplierAddress
{
    public class SupplierAddressDTOUpdate : SupplierAddressDTO
	{
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
       
        public SupplierAddressDTOUpdate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
