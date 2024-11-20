
namespace DeviceService.Application.DTOS.SupplierAccount
{
    public class SupplierAccountDTO
	{
        public long? ID { get; set; }
		public long? SupplierId { get; set; }
        public string Name { get; set; }
        public string BankNumber { get; set; }
		public string BankCode { get; set; }
		public string BankName { get; set; }
		public string BankAddress { get; set; }
		public string? BicCode { get; set; }
		public string? SwiftAddress { get; set; }
		public string? IBAN { get; set; }
		public bool? IsDefault { get; set; }
    }
}
												