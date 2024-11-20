using Microsoft.AspNetCore.Mvc;
using DeviceService.Application.DTOS.Account;
using DeviceService.Application.DTOS.Users;
using DeviceService.Application.Interfaces;
using DeviceService.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Device.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseAnonymousController
    {
        #region Properties
        private readonly IAccountServices _accountServices;
        private readonly IConfiguration _configuration;
		#endregion
		#region Ctor
		public AuthController( IConfiguration configuration
        , IAccountServices accountServices)
        {
            _configuration = configuration;
            _accountServices = accountServices;
		}
        #endregion
        /// <summary>
        /// Login
        /// </summary>
        /// <returns>Token</returns>  
        [SwaggerOperation(Summary = "Đăng nhập")]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var rs = await _accountServices.Login(model);
            var serviceResult = rs;
            return JSonResult(rs);
        }
        [SwaggerOperation(Summary = "Refresh Token")]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] LogoutDTO model)
        {
            var rs = await _accountServices.RefreshToken(model);
            var serviceResult = rs;
            return JSonResult(rs);
        }
		[SwaggerOperation(Summary = "Gửi yêu cầu và tạo mới nhà cung cấp")]
		[HttpPost("create-user-request")]
		public async Task<IActionResult> CreateUserRequest([FromBody]  UsersRequestDTO usersRequest)
		{
			return JSonResult(await _accountServices.SendRequestAsync(usersRequest));
		}
    }
}
