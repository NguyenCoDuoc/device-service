using Portal.Application.DTOS.QCInspectionItemCode;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace Portal.Application.Interfaces
{
    public interface IQCInspectionItemCodeServices
    {
        /// <summary>
        /// Danh sách 
        /// </summary> 
        /// <param name="pagingParams"></param>
        Task<PagingResult<InspectionDashboardDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        Task<ServiceResult> GetByIdAsync(long id);

       

      

    }
}
