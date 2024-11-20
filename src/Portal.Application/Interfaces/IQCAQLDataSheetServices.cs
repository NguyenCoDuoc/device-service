using Portal.Application.DTOS.CicmpyConsolidated;
using Portal.Application.DTOS.QCAQLDataSheet;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace Portal.Application.Interfaces
{
	public interface IQCAQLDataSheetServices
    {
		/// <summary>
		/// Danh sách 
		/// </summary> 
		/// <param name="pagingParams"></param>
		Task<PagingResult<QCAQLDataSheetDTO>> GetPagingAsync(SearchParam pagingParams);

		/// <summary>
		/// Chi tiết 1 bản ghi
		/// </summary>
		/// <param name="id"></param>
		Task<ServiceResult> GetByIdAsync(string id);
	}
}
