
namespace DeviceService.Application.DTOS.SupplierContact
{
    public class SupplierContactDTO
	{
        public long? ID { get; set; }
        public long? SupplierId { get; set; }
        public string Name { get; set; }
		public int? Gender { get; set; }
		public string? Department { get; set; }
		public string? Position { get; set; }
		public string? Mobile { get; set; }
		public string? Fax { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? Manager { get; set; }
		public string? Remark { get; set; }
	}
}
