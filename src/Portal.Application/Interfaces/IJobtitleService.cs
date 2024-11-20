using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IJobtitleService
    {
        /// <summary>
        /// Danh sách  vị trí, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 20240916
        Task<PagingResult<JobtitleDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo  vị trí
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> CreateAsync(JobtitleDtoCreate model);

        /// <summary>
        /// Cập nhật  vị trí
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<JobtitleDto> UpdateAsync(JobtitleDto model);

        /// <summary>
        /// Chi tiết  vị trí
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa  vị trí
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<bool> DeleteAsync( long id);
    }
}