using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Domain.Entities
{
	[Table("supplier_account")]
	public class SupplierAccount : CrudFieldEntity<long>
	{
		[Column("supplier_id")]
		public long SupplierId { get; set; }
		[Column("bank_number")]
		public string BankNumber { get; set; }
		[Column("bank_code")]
		public string BankCode { get; set; }
		[Column("bank_name")]
		public string BankName { get; set; }
		[Column("bic_code")]
		public string BicCode { get; set; }
		[Column("swift_addrress")]
		public string SwiftAddress { get; set; }
		public string Name { get; set; }
		public string IBAN { get; set; }
		[Column("is_default")]
		public bool IsDefault { get; set; }
        [Column("bank_address")]
        public string BankAddress { get; set; }
    }
}
