using DeviceService.Application.DTOS.State;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
namespace DeviceService.Application.Interfaces
{
    public interface IStateServices
    {
        /// <summary>
        /// Danh sách bảng phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        Task<PagingResult<StateDTO>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Chi tiết bản ghi
        /// </summary>
        /// <param name="id"></param>
        Task<PagingResult<StateDTO>> GetPagingAsync(long CountryId, SearchParam pagingParams);



    }
}
