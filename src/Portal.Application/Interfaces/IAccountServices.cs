using DeviceService.Application.DTOS.Account;
using DeviceService.Application.DTOS.QCInspectionRequest;
using DeviceService.Application.DTOS.Users;
using Sun.Core.Share.Helpers.Results;

namespace DeviceService.Application.Interfaces
{
    public interface IAccountServices
    {
        Task<ServiceResult> Login(LoginDTO model);
        Task<ServiceResult> Logout(long ID, string refresh_token);
        Task<ServiceResult> GetInfo(long ID);
        Task<ServiceResult> RefreshToken(LogoutDTO model);
        Task<ServiceResult> ChangePassword(ChangePasswordDTO model, long id);
        Task<ServiceResult> UpdateProfile(UpdateProfileDTO model, long id);
        Task<ServiceResult> SendRequestAsync(UsersRequestDTO model);
    }
}
