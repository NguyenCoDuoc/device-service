using Microsoft.AspNetCore.Mvc;
using DeviceService.Application.ContextAccessors;
using DeviceService.Application.DTOS.Account;
using DeviceService.Application.DTOS.Users;
using DeviceService.Application.Interfaces;
using DeviceService.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Device.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseAuthorizeController
    {
        #region Properties
        private readonly IAccountServices _accountServices;
        private readonly IUserPrincipalService _CurrentUser;
        private readonly IConfiguration _configuration;
        #endregion
        #region Ctor
        public AccountController(IConfiguration configuration
        , IAccountServices accountServices, IUserPrincipalService userIdentity)
        {
            _configuration = configuration;
            _accountServices = accountServices;
            _CurrentUser= userIdentity;
        }
        #endregion
        //// <summary>
        //// GetInfo
        //// </summary>
        //// <returns>Token</returns>  
        [SwaggerOperation(Summary = "Get Info")]
        [HttpGet]
        [Route("get-info")]
        public async Task<IActionResult> GetInfo()
        {
            return JSonResult(await _accountServices.GetInfo(_CurrentUser.UserId));
        }
		//// <summary>
		//// Change password
		//// </summary>
		//// <returns>Change password</returns>
		[SwaggerOperation(Summary = "Đổi mật khẩu")]
		[HttpPut]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            return JSonResult(await _accountServices.ChangePassword(model,_CurrentUser.UserId));
        }

		//// <summary>
		//// Update profile
		//// </summary>
		//// <returns>Update profile</returns>
		[SwaggerOperation(Summary = "Cập nhật thông tin")]
		[HttpPut]
        [Route("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO model)
        {
            return JSonResult(await _accountServices.UpdateProfile(model, _CurrentUser.UserId));
        }

        [SwaggerOperation(Summary = "Đăng xuất")]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return JSonResult(await _accountServices.Logout(_CurrentUser.UserId, _CurrentUser.RefreshToken));
        }
    }
}
