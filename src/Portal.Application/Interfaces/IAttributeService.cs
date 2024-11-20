using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IAttributeService
    {
        /// <summary>
        /// Danh sách  thuộc tính, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 20240916
        Task<PagingResult<AttributeDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo  thuộc tính
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> CreateAsync(AttributeDtoCreate model);

        /// <summary>
        /// Cập nhật  thuộc tính
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<AttributeDto> UpdateAsync(AttributeDto model);

        /// <summary>
        /// Chi tiết  thuộc tính
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa  thuộc tính
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<bool> DeleteAsync( long id);

        /// <summary>
        /// Lấy all thuộc tính
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AttributeDtoDetail>> GetAllAsync();
    }
}