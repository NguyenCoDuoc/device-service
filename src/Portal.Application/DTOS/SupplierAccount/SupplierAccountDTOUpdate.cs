namespace DeviceService.Application.DTOS.SupplierAccount
{
    public class SupplierAccountDTOUpdate : SupplierAccountDTO
	{
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
       
        public SupplierAccountDTOUpdate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
