using Portal.Application.DTOS.QCInspectionRequest;
using Portal.Application.DTOS.QCInspectionRequestDetail;
using Portal.Application.DTOS.QCInspectionRequestDetailResult;
using Sun.Core.Share.Helpers.Results;


namespace Portal.Application.Interfaces
{
    public interface IQCInspectionRequestDetailServices
	{
		Task<PagingResult<QCInspectionRequestDetailDTO>> GetPagingAsync(QCInspectionRequestSearchParam pagingParams);
		Task<ServiceResult> GetByItem(string ItemCode, double LotSize);
		Task<ServiceResult> CreateAsync(QCInspectionRequestDetailDTOUpdate detail, List<QCInspectionRequestDetailResultDTOCreate> detailTemplates);
		Task<ServiceResult> UpdateStatusAsync(string Id, string Status);
		Task<ServiceResult> CloneAsync(string Id);
		Task<ServiceResult> UpdateDetailResultAsync(QCInspectionRequestDetailResultDTOUpdate model);

    }
}
