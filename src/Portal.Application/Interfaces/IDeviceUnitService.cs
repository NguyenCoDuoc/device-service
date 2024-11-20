using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IDeviceUnitService
    {
        /// <summary>
        /// Danh sách loại thiết bị, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 20240916
        Task<PagingResult<DeviceUnitDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo loại thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> CreateAsync(DeviceUnitDtoCreate model);

        /// <summary>
        /// Cập nhật loại thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<DeviceUnitDto> UpdateAsync(DeviceUnitDto model);

        /// <summary>
        /// Lấy all loại thiết bị
        /// </summary>
        /// DUOCNC 20240916
        Task<IEnumerable<DeviceUnitDtoDetail>> GetAllAsync();

        /// <summary>
        /// Chi tiết loại thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa loại thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<bool> DeleteAsync( long id);
    }
}