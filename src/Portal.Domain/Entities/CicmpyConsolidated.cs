using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Entities
{
	[Table("Cicmpy_Consolidated")]
	public class CicmpyConsolidated
	{
		[Key]
		public string cmp_wwn { get; set; }

		public int Division { get; set; }

		public string cmp_code { get; set; }

		public string cmp_name { get; set; }

		public string cmp_fadd1 { get; set; }

		public string cmp_inv_address { get; set; }

		public string cmp_del_address { get; set; }

		public string cmp_tel { get; set; }

		public string cmp_fcity { get; set; }

		public string cmp_email { get; set; }

		public string StateCode { get; set; }

		public int? SHProvinceId { get; set; }

		public int? SHDistrictId { get; set; }

		public int? SHCommuneId { get; set; }

		public bool SaleforcastOff { get; set; }

		public string cmp_fctry { get; set; }

		public int? employee { get; set; }

		public string ClassificationId { get; set; }

		public string debnr { get; set; }

		public string debnr_102 { get; set; }

		public string crdnr { get; set; }

		public string debcode { get; set; }

		public string crdcode { get; set; }

		public string costcenter { get; set; }

		public string Status { get; set; }

		public string bankAccountNumber { get; set; }

		public string PaymentMethod { get; set; }

		public string InvoiceDebtor { get; set; }

		public string VatNumber { get; set; }

		public string PaymentCondition { get; set; }

		public string Pricelist { get; set; }

		public string cmp_type { get; set; }

		public int? cmp_acc_man { get; set; }

		public string cmp_status { get; set; }

		public string siz_code { get; set; }

		public string TextField2 { get; set; }

		public string textfield11 { get; set; }

		public string MTGroupCode { get; set; }

		public string MTGroupNetPrice { get; set; }

		public string MTGroupQuotaPrice { get; set; }

		public string Longitude { get; set; }

		public string Latitude { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string ModifyBy { get; set; }

		public string TextField6 { get; set; }

		public string VatCode { get; set; }

		public bool LockLocation { get; set; }

		public string cmp_wwn_101 { get; set; }

		public string cmp_wwn_2021 { get; set; }

		public string DbSource { get; set; }

		public string Warehouse { get; set; }

		public string PricelistPromo { get; set; }

		public string DefaultSelCode { get; set; }

		public bool Active { get; set; }
	}
}
