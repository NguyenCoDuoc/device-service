using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities
{
    [Description("Bảng thông tin chi tiết phiếu kiểm thử")]
    [Table("QCInspectionRequestDetail")]
    public class QCInspectionRequestDetail
    {
        [Key]
		public string Id { get; set; }

		public string MasterId { get; set; }

		public string ordernr { get; set; }

		public DateTime? afldat { get; set; }

		public string InspectionNo { get; set; }
		public string Status { get; set; }

		public string ItemCode { get; set; }

		public string ItemName { get; set; }

		public string Model { get; set; }

		public double LotSize { get; set; }

		public double? LotQuantity { get; set; }

		public double? InspectionQuantity { get; set; }

		public DateTime? NeedDate { get; set; }

		public string Decision { get; set; }

		public bool Active { get; set; }
		public string RefId { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Create)]
        public string CreateBy { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Create)]
        public DateTime CreateDate { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Update)]
        public string? ModifyBy { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Update)]
        public DateTime? ModifyDate { get; set; }
		public string Syslog { get; set; }
		public string QCTestType { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string InspectionLocation { get; set; }
		public string ErrorReason { get; set; }
		public string InspectionNote { get; set; }
		public string InspectionSummary { get; set; }
		public DateTime? InspectionDate { get; set; }
		public string InspectionBy { get; set; }



	}
}
