using Sun.Core.DataAccess.Helpers.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities
{
    [Description("Bảng thông tin chi tiết phiếu kết quả")]
    [Table("QCInspectionRequestDetailResult")]
    public class QCInspectionRequestDetailResult
	{
		[Key]
		public string Id { get; set; }

		public string MasterId { get; set; }

		public string DetailId { get; set; }

		public string InspectionGroupCode { get; set; }

		public string InspectionGroupName { get; set; }

		public string ShortDescription { get; set; }

		public string InspectionItemCode { get; set; }

		public string InspectionItemName { get; set; }

		public string InspectionDescription { get; set; }

		public string LetterCode { get; set; }

		public double SampleSize { get; set; }

		public string StandardValue { get; set; }

		public double Inspection { get; set; }

		public double Ac { get; set; }

		public double Re { get; set; }

		public double OK { get; set; }

		public double NG { get; set; }

		public string Decision { get; set; }

		public string DecisionNote { get; set; }

		public string OverwriteDecision { get; set; }

		public string OverwriteDecisionNote { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Create)]
        public string CreateBy { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Create)]
        public DateTime CreateDate { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Update)]
        public string ModifyBy { get; set; }
        [CrudField(true, true, UsedFor = CrudFieldType.Update)]
        public DateTime? ModifyDate { get; set; }

		public string Syslog { get; set; }
	}
}
