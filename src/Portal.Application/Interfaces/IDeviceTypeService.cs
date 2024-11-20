using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IDeviceTypeService
    {
        /// <summary>
        /// Danh sách loại thiết bị, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 30072024
        Task<PagingResult<DeviceTypeDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo loại thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 30072024
        Task<ServiceResult> CreateAsync(DeviceTypeDtoCreate model);

        /// <summary>
        /// Cập nhật loại thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 30072024
        Task<DeviceTypeDto> UpdateAsync(DeviceTypeDto model);

        /// <summary>
        /// Lấy all loại thiết bị
        /// </summary>
        /// DUOCNC 30072024
        Task<IEnumerable<DeviceTypeDtoDetail>> GetAllAsync();

        /// <summary>
        /// Chi tiết loại thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 30072024
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa loại thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 30072024
        Task<bool> DeleteAsync( long id);

        /// <summary>
        /// Get device type attributes by device type id
        /// </summary>
        /// <param name="deviceTypeId">Device type id</param>
        /// <returns>List of device type attributes</returns>
        Task<List<DeviceTypeAttributeDto>> GetDeviceTypeAttributes(long deviceTypeId);

        /// <summary>
        /// Add device type attribute
        /// </summary>
        /// <param name="deviceTypeAttribute">Device type attribute</param>
        /// <returns>Device type attribute id</returns>
        /// DUOCNC 20241106
        Task<int> AddDeviceTypeAttribute(DeviceTypeAttributeDto deviceTypeAttribute);

        /// <summary>
        /// Delete device type attribute
        /// </summary>
        /// <param name="id">Device type attribute id</param>
        Task<int> DeleteDeviceTypeAttribute(long id);
    }
}