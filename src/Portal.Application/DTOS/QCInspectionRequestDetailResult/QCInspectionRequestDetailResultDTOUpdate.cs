using Portal.Application.DTOS.QCInspectionRequestDetailResult;

namespace Portal.Application.DTOS.QCInspectionRequest
{
    public class QCInspectionRequestDetailResultDTOUpdate : QCInspectionRequestDetailResultDTO
    {
        public DateTime ModifyDate { get; set; }
        public QCInspectionRequestDetailResultDTOUpdate()
        {
            ModifyDate = DateTime.UtcNow;
        }
    }
}
