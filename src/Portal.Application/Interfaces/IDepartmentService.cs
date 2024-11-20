using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Danh sách phòng ban, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 30072024
        Task<PagingResult<DepartmentDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo phòng ban
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 30072024
        Task<ServiceResult> CreateAsync(DepartmentDtoCreate model);

        /// <summary>
        /// Cập nhật phòng ban
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 30072024
        Task<DepartmentDto> UpdateAsync(DepartmentDto model);

        /// <summary>
        /// Lấy all phòng ban
        /// </summary>
        /// DUOCNC 30072024
        Task<IEnumerable<DepartmentDtoDetail>> GetAllAsync();

        /// <summary>
        /// Chi tiết phòng ban
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 30072024
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 30072024
        Task<bool> DeleteAsync( long id);

        /// <summary>
        /// Danh sách phòng ban theo user id
        /// </summary> 
        /// <param name="userid"></param>
        /// DUOCNC 20082024
        Task<List<DepartmentDtoDetail>> GetDepartmentsByUserId(long userId);
    }
}