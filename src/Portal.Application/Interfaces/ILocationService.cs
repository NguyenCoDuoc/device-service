using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface ILocationService
    {
        /// <summary>
        /// Danh sách vị trí, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 30072024
        Task<PagingResult<LocationDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo vị trí
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 30072024
        Task<ServiceResult> CreateAsync(LocationDtoCreate model);

        /// <summary>
        /// Cập nhật vị trí
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 30072024
        Task<LocationDto> UpdateAsync(LocationDto model);

        /// <summary>
        /// Lấy all vị trí
        /// </summary>
        /// DUOCNC 30072024
        Task<IEnumerable<LocationDtoDetail>> GetAllAsync();

        /// <summary>
        /// Chi tiết vị trí
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 30072024
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa vị trí
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 30072024
        Task<bool> DeleteAsync( long id);
    }
}