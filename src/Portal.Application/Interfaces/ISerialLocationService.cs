using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface ISerialLocationService
    {
        /// <summary>
        /// Danh sách  SerialLocation, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 20240916
        Task<PagingResult<SerialLocationDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo  SerialLocation
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> CreateAsync(SerialLocationDtoCreate model);

        /// <summary>
        /// Cập nhật  SerialLocation
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<SerialLocationDto> UpdateAsync(SerialLocationDto model);

        /// <summary>
        /// Chi tiết  SerialLocation
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa  SerialLocation
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<bool> DeleteAsync( long id);
    }
}