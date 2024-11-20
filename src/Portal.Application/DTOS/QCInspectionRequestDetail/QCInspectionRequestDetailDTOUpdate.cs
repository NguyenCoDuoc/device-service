namespace Portal.Application.DTOS.QCInspectionRequestDetail
{
    public class QCInspectionRequestDetailDTOUpdate
    {
        public string Id { get; set; }
        public double LotSize { get; set; }
        public double LotQuantity { get; set; }
        public double InspectionQuantity { get; set; }
		public string QCTestType { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string InspectionLocation { get; set; }
		public string? ErrorReason { get; set; }
		public string? InspectionNote { get; set; }
		public string? InspectionSummary { get; set; }
		public DateTime InspectionDate { get; set; }
		public string InspectionBy { get; set; }
    }
}
