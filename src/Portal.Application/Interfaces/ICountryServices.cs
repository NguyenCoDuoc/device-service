using DeviceService.Application.DTOS.Country;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
namespace DeviceService.Application.Interfaces
{
    public interface ICountryServices
	{
        /// <summary>
        /// Danh sách bảng phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        Task<PagingResult<CountryDTO>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Chi tiết bản ghi
        /// </summary>
        /// <param name="id"></param>
        Task<ServiceResult> GetByIdAsync(long id);

       
	}
}
