using Sun.Core.DataAccess.Helpers.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceService.Domain.Entities
{
	// Bảng quốc gia
    [Table("countries")]
    public class Country : CrudFieldEntity<long>
	{
		[Description("Tên quốc gia")]
		[Column("name")]
		public string Name { get; set; }

		[Description("Mã số quốc gia dạng số")]
		[Column("numeric_code")]
		public string NumericCode { get; set; }

		[Description("Mã quốc gia dạng chữ")]
		[Column("code")]
		public string Code { get; set; }

		[Description("Mã điện thoại quốc gia")]
		[Column("phonecode")]
		public string PhoneCode { get; set; }

		[Description("Thủ đô")]
		[Column("capital")]
		public string Capital { get; set; }

		[Description("Loại tiền tệ")]
		[Column("currency")]
		public string Currency { get; set; }

		[Description("Tên loại tiền tệ")]
		[Column("currency_name")]
		public string CurrencyName { get; set; }

		[Description("Ký hiệu loại tiền tệ")]
		[Column("currency_symbol")]
		public string CurrencySymbol { get; set; }

		[Description("Tên bản địa của quốc gia")]
		[Column("native")]
		public string Native { get; set; }

		[Description("Khu vực của quốc gia")]
		[Column("region")]
		public string Region { get; set; }

		[Description("ID khu vực của quốc gia")]
		[Column("region_id")]
		public long? RegionId { get; set; }

		[Description("Quốc tịch")]
		[Column("nationality")]
		public string Nationality { get; set; }

		[Description("Vĩ độ")]
		[Column("latitude")]
		public decimal? Latitude { get; set; }

		[Description("Kinh độ")]
		[Column("longitude")]
		public decimal? Longitude { get; set; }
	}
}
