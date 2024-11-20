using Sun.Core.DataAccess.Helpers.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceService.Domain.Entities
{
    [Table("supplier")]
    public class Supplier : CrudFieldEntity<long>
	{
		public string cmp_wwn { get; set; }
        [Description("Pháp nhân")]
        public int Division { get; set; }
        [Description("Mã nhà cung cấp")]
		public string Code { get; set; }
		[Description("Tên nhà cung cấp")]
        public string Name { get; set; }
        [Description("Email")]
        public string Email { get; set; }
        
        [Description("Số điện thoại")]
        public string Phone { get; set; }
        [Description("Địa chỉ")]
        public string Address { get; set; }
        [Description("Trạng thái")]
        public int? Status { get; set; }
        [Description("Mô tả")]
        public string Description { get; set; }
		
		[Description("ID quốc gia")]
        [Column("country_id")]
        public long? CountryId { get; set; }
        [Description("ID của bang hoặc tiểu bang")]
        [Column("state_id")]
        public long? StateId { get; set; }
        [Description("ID của thành phố/huyện/quận")]
        [Column("city_id")]
        public long CityId { get; set; }
		[Description("Mã bưu chính")]
		[Column("postal_code")]
		public string PostalCode { get; set; }
		[Description("Mã số thuế")]
		[Column("vat_number")]
		public string VatNumber { get; set; }
		[Description("Loại tiền tệ")]
		public string Currency { get; set; }
		[Description("Fax")]
		public string Fax { get; set; }
		[Description("Website")]
		public string Website { get; set; }
        [Description("Điều kiện thanh toán")]
        [Column("payment_condition")]
        public string PaymentCondition { get; set; }
	}
}
