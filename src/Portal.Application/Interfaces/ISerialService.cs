using DeviceService.Application.DTOS;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface ISerialService
    {
        /// <summary>
        /// Danh sách  Serial, phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        /// DUOCNC 20240916
        Task<PagingResult<SerialDto>> GetPagingAsync(SearchParam pagingParams);

        /// <summary>
        /// Tạo  Serial
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> CreateAsync(SerialDtoCreate model);

        /// <summary>
        /// Cập nhật  Serial
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        Task<SerialDto> UpdateAsync(SerialDto model);

        /// <summary>
        /// Chi tiết  Serial
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<ServiceResult> GetByIdAsync( long id);

        /// <summary>
        /// Xóa  Serial
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        Task<bool> DeleteAsync( long id);

        Task<List<SerialAttributeDto>> GetSerialAttributes(long serial);

        Task<int> AddSerialAttribute(SerialAttributeDto serialAttribute);


        Task<int> DeleteSerialAttribute(long id);
    }
}