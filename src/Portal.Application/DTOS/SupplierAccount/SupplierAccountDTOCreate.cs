namespace DeviceService.Application.DTOS.SupplierAccount
{
    public class SupplierAccountDTOCreate : SupplierAccountDTO
	{
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
       
        public SupplierAccountDTOCreate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
