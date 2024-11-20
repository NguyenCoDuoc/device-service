using DeviceService.Application.DTOS.City;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
namespace DeviceService.Application.Interfaces
{
    public interface ICityServices
    {
        /// <summary>
        /// Danh sách bảng phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        Task<PagingResult<CityDTO>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Chi tiết bản ghi
        /// </summary>
        /// <param name="id"></param>
        Task<PagingResult<CityDTO>> GetPagingAsync(long CountryId, long StateId, SearchParam pagingParams);



    }
}
