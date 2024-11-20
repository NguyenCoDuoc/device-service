using Portal.Application.DTOS.QCInspectionRequestDetailResult;

namespace Portal.Application.DTOS.QCInspectionRequestDetail
{
    public class QCInspectionRequestDetailDTO
    {
        public string Id { get; set; }
        public string MasterId { get; set; }
		public string cmp_wwn { get; set; }
		public string cmp_name { get; set; }
		public string RequestNumber { get; set; }
		public string InspectionNo { get; set; }

		public string ordernr { get; set; }

        public DateTime afldat { get; set; }

        public string Status { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string Model { get; set; }

        public double LotSize { get; set; }

        public double? LotQuantity { get; set; }

        public double? InspectionQuantity { get; set; }

        public DateTime NeedDate { get; set; }

        public string Decision { get; set; }
        public string KQ { get; set; }
		public string CreateBy { get; set; }

		public DateTime CreateDate { get; set; }
		public string QCTestType { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string InspectionLocation { get; set; }
		public string ErrorReason { get; set; }
		public string InspectionNote { get; set; }
		public string InspectionSummary { get; set; }
		public DateTime? InspectionDate { get; set; }
		public string InspectionBy { get; set; }

		public List<QCInspectionRequestDetailResultDTO> DetailResults { get; set; }

    }
}
