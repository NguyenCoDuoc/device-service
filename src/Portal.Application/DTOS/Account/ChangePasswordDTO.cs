
namespace DeviceService.Application.DTOS.Account
{
    public class ChangePasswordDTO
    {
        public string Password { set; get; }
        public string NewPassword { set; get; }
        public string ConfirmPassword { get; set; }
    }
}
