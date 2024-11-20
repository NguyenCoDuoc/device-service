using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IAttributeValueService
    {
        /// <summary>
        /// Danh sách  thuộc tính giá trị, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 20240916
        Task<PagingResult<AttributeValueDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo  thuộc tính giá trị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> CreateAsync(AttributeValueDtoCreate model);

        /// <summary>
        /// Cập nhật  thuộc tính giá trị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<AttributeValueDto> UpdateAsync(AttributeValueDto model);

        /// <summary>
        /// Chi tiết  thuộc tính giá trị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa  thuộc tính giá trị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<bool> DeleteAsync( long id);

        /// <summary>
        /// Lấy all thuộc tính giá trị
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AttributeValueDtoDetail>> GetAllAsync();
    }
}