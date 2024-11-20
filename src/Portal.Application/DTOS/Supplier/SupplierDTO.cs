using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DeviceService.Application.DTOS.Supplier
{
    public class SupplierDTO
	{
        public long rownumber { get; set; }
        public long ID { get; set; }
        public int? Division { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Code { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
		public string? cmp_wwn { get; set; }
		public int? Status { get; set; }
		public string? PostalCode { get; set; }
		public string? VatNumber { get; set; }
		public string? Currency { get; set; }
		public string? Fax { get; set; }
		public string? Website { get; set; }
        public string? PaymentCondition { get; set; }
        public long? CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }

    }
}
