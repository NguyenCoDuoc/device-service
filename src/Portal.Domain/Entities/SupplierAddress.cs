using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Domain.Entities
{
    [Table("supplier_address")]
    public class SupplierAddress : CrudFieldEntity<long>
    {
        [Column("supplier_id")]
        public long SupplierId { get; set; }
       
        public string Type { get; set; }
        [Description("ID quốc gia")]
        [Column("country_id")]
        public long? CountryId { get; set; }
        [Description("ID của bang hoặc tiểu bang")]
        [Column("state_id")]
        public long? StateId { get; set; }
        [Description("ID của thành phố/huyện/quận")]
        [Column("city_id")]
        public long CityId { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
		[Column("postal_code")]
		public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Remark { get; set; }
        [Column("contact_name")]
        public string ContactName { get; set; }
        [Column("contact_phone")]
        public string ContactPhone { get; set; }
		[Column("is_default")]
		public bool IsDefault { get; set; }
	}
}
