using DeviceService.Application.DTOS;
using DeviceService.Application.DTOS.Users;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IUserService
    {
       
        Task<PagingResult<UsersDTO>> GetPagingAsync(SearchParam pagingParams);

        Task<ServiceResult> CreateAsync(UsersDTOCreate model);

        Task<UsersDTO> UpdateAsync(UsersDTO model);

        Task<ServiceResult> GetByIdAsync( long id);

        Task<bool> DeleteAsync( long id);

        Task<IEnumerable<UsersDTO>> GetAllAsync();
    }
}