using Portal.Application.DTOS.QCInspectionRequestDetailResult;

namespace Portal.Application.DTOS.QCInspectionRequest
{
    public class QCInspectionRequestDetailResultDTOCreate : QCInspectionRequestDetailResultDTO
    {
        public DateTime CreateDate { get; set; }
        public QCInspectionRequestDetailResultDTOCreate()
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}
