using Portal.Application.DTOS.QCInspectionRequest;
using Portal.Application.DTOS.Users;
using Sun.Core.Share.Helpers.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
	public interface IQCInspectionRequestServices
	{
		Task<PagingResult<QCInspectionRequestMasterDTO>> GetPagingAsync(QCInspectionRequestSearchParam pagingParams);
		Task<ServiceResult> GetByIdAsync(string Id);
		Task<ServiceResult> GetByIdAsync(string Id, string DetailId);
		Task<ServiceResult> UpdateStatusAsync(QCInspectionRequestStatusDTO model);

	}
}
