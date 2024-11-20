
namespace DeviceService.Application.DTOS.SupplierAddress
{
    public class SupplierAddressDTO
	{
        public long? ID { get; set; }
        public long? SupplierId { get; set; }
		public string? Type { get; set; }
		public string Address { get; set; }
		public string? Address1 { get; set; }
		public string? PostalCode { get; set; }
		public string? Phone { get; set; }
		public string? Fax { get; set; }
		public string? Remark { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
		public bool? IsDefault { get; set; } = false;
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long CityId { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
    }
}
