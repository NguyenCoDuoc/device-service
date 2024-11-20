using Portal.Application.DTOS.QCInspectionGroup;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace Portal.Application.Interfaces
{
    public interface IQCInspectionGroupServices
    {
        /// <summary>
        /// Danh sách 
        /// </summary> 
        /// <param name="pagingParams"></param>
        Task<PagingResult<QCInspectionGroupDTO>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Chi tiết 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        Task<ServiceResult> GetByCodeAsync(string Code);

       

      

    }
}
