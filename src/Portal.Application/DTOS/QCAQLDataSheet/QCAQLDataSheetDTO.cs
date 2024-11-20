
namespace Portal.Application.DTOS.QCAQLDataSheet
{
    public class QCAQLDataSheetDTO
    {
        public string Id { get; set; }
        public string InspectionLever { get; set; }
        public double LotSizeFrom { get; set; }
        public double LotSizeTo { get; set; }
        public string LetterCode { get; set; }
        public double SampleSize { get; set; }
        public double Inspection { get; set; }
        public double Ac { get; set; }
        public double Re { get; set; }
        public double? P_SampleSize { get; set; }
    }
}
