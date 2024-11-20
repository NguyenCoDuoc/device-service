using DeviceService.Application.DTOS.Supplier;
using DeviceService.Application.DTOS.SupplierAccount;
using DeviceService.Application.DTOS.SupplierAddress;
using DeviceService.Application.DTOS.SupplierContact;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface ISupplierServices
	{
        /// <summary>
        /// Danh sách người dùng phân trang
        /// </summary> 
        /// <param name="pagingParams"></param>
        Task<PagingResult<SupplierDTO>> GetPagingAsync(SupplierSearchParam pagingParams);

        /// <summary>
        /// Chi tiết người dùng
        /// </summary>
        /// <param name="id"></param>
        Task<ServiceResult> GetByIdAsync(long id);

        /// <summary>
        /// thêm mới
        /// </summary>
        /// <param name="model"></param>
        Task<ServiceResult> CreateAsync(SupplierDTOCreate model);

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="model"></param>
        Task<ServiceResult> UpdateAsync(SupplierDTOUpdate model);

        Task<ServiceResult> UpdateStatusAsync(long id, int Status);

		/// <summary>
		/// Xóa người dùng
		/// </summary>
		/// <param name="id"></param>
		Task<ServiceResult> DeleteAsync(long id);

        Task<ServiceResult> GetByContactAsync(long SupplieId);

		Task<ServiceResult> CreateContactAsync(long id, SupplierContactDTOCreate model);
        Task<ServiceResult> UpdateContactAsync(long id, SupplierContactDTOUpdate model);
        Task<ServiceResult> DeleteContactAsync(long id);
        Task<ServiceResult> GetByAddressAsync(long SupplieId);

		Task<ServiceResult> CreateAddressAsync(long id, SupplierAddressDTOCreate model);
        Task<ServiceResult> UpdateAddressAsync(long id, SupplierAddressDTOUpdate model);
        Task<ServiceResult> DeleteAddressAsync(long id);
        Task<ServiceResult> GetByAccountAsync(long SupplieId);

		Task<ServiceResult> CreateAccountAsync(long id, SupplierAccountDTOCreate model);
        Task<ServiceResult> UpdateAccountAsync(long id, SupplierAccountDTOUpdate model);
        Task<ServiceResult> DeleteAccountAsync(long id);




	}
}
