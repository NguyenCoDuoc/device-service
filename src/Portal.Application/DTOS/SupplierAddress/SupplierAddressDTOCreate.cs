
namespace DeviceService.Application.DTOS.SupplierAddress
{
    public class SupplierAddressDTOCreate : SupplierAddressDTO
	{
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
       
        public SupplierAddressDTOCreate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
