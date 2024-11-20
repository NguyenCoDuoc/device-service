using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IDeviceService
    {
        /// <summary>
        /// Danh sách  thiết bị, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 20240916
        Task<PagingResult<DeviceDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo  thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> CreateAsync(DeviceDtoCreate model);

        /// <summary>
        /// Cập nhật  thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<DeviceDto> UpdateAsync(DeviceDto model);

        /// <summary>
        /// Lấy all  thiết bị
        /// </summary>
        /// DUOCNC 20240916
        Task<IEnumerable<BaseDto>> GetAllAsync();

        /// <summary>
        /// Chi tiết  thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa  thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<bool> DeleteAsync( long id);
    }
}