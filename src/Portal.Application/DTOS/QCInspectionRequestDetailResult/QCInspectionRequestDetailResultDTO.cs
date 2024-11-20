
namespace Portal.Application.DTOS.QCInspectionRequestDetailResult
{
    public class QCInspectionRequestDetailResultDTO
    {
        public string? Id { get; set; }

        public string? MasterId { get; set; }

        public string? DetailId { get; set; }

        public string InspectionGroupCode { get; set; }

        public string InspectionGroupName { get; set; }

        public string? ShortDescription { get; set; }

        public string InspectionItemCode { get; set; }

        public string InspectionItemName { get; set; }

        public string? InspectionDescription { get; set; }
        public string? InspectionMethod { get; set; }

        public string? LetterCode { get; set; }

        public double SampleSize { get; set; }

        public string? StandardValue { get; set; }

        public double Inspection { get; set; }
        public bool FromTemp { get; set; }
        public string? InspectionLever { get; set; }
        public double LotSizeFrom { get; set; }
        public double LotSizeTo { get; set; }

        public double Ac { get; set; }

        public double Re { get; set; }

        public double OK { get; set; }

        public double NG { get; set; }

        public string? Decision { get; set; }

        public string? DecisionNote { get; set; }

        public string? OverwriteDecision { get; set; }

        public string? OverwriteDecisionNote { get; set; }
        public string InspectionGroupNameCN { get; set; }
        public string InspectionGroupNameEN { get; set; }
        public string InspectionItemNameCN { get; set; }
        public string InspectionItemNameEN { get; set; }
        public string InspectionDescriptionCN { get; set; }
        public string InspectionDescriptionEN { get; set; }

    }
}
