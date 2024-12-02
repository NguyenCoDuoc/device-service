using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DeviceService.Application.DTOS.Account;
using DeviceService.Application.DTOS.Options;
using DeviceService.Application.DTOS.QCInspectionRequest;
using DeviceService.Application.DTOS.Users;
using DeviceService.Application.DTOS.UsersSession;
using DeviceService.Application.Helpers.Enums;
using DeviceService.Application.Interfaces;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Dapper.SqlMapper;
using static DeviceService.Application.Helpers.Enums.EnumCommon;

namespace DeviceService.Application.Services
{
    public class AccountServices : IAccountServices
    {
        #region Properties
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersSessionRepository _usersSessionRepository;
        private readonly IMapper _mapper;
        private IOptions<JWTAudience> _settings;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        #endregion
        #region Ctor
        public AccountServices(IOptions<JWTAudience> settings, IUsersRepository usersRepository,
            IUsersSessionRepository usersSessionRepository,
            IMapper mapper,
            ILoggerManager logger,
            IConfiguration configuration)
        {
            _settings = settings;
            _usersRepository = usersRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _usersSessionRepository = usersSessionRepository;
        }
        #endregion
        #region Login
        public async Task<ServiceResult> Login(LoginDTO model)
        {
            var rs = new ServiceResult();
            if (model.Password.Length > 255 || model.Password.Length < 6)
            {
                return new ServiceResultError("Password must be between 6 and 255 characters");
            }
            var user = await _usersRepository.GetFieldAsync("user_name", model.UserName);
            if (user == null || user.ID <= 0)//Nếu không tìm thấy theo tên đăng nhập thì tìm user theo email
            {
                user = await _usersRepository.GetFieldAsync("email", model.UserName);
            }
            if (Utils.IsEmpty(user)) //Đăng nhập sai & hoặc account không tồn tại
            {
                rs = new ServiceResultError("Account information is incorrect");
                return rs;
            }
            else
            {
                var userDTO = _mapper.Map<UsersDTO>(user);
                if (!Hash.IsMatch(model.Password, user.PasswordHash))
                {
                    rs = new ServiceResultError("Username or password is incorrect");
                    return rs;
                }
                //Check Status
                if (userDTO.Status == (int)EnumCommon.StatusUsers.Locked)
                {
                    rs = new ServiceResultError("Your account has been locked");
                    return rs;
                }
                // Tạo sesion mỗi lần khi đăng nhập
                var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

                await CreateUserSession(userDTO, model.RememberMe, refresh_token);
                // tạo token
                var response = GenerateJwtToken(userDTO, model.RememberMe, refresh_token);
                rs = new ServiceResultSuccess("Login successful", response);
                return rs;
            }

        }
        #endregion
        #region Logout
        public async Task<ServiceResult> Logout(long ID, string refresh_token)
        {
            var rs = new ServiceResult();
            var user = await _usersRepository.GetAsync(ID);
            if (Utils.IsEmpty(user))
            {
                rs = new ServiceResultError("This information does not exist");
                return rs;
            }
            var usersSession = await _usersSessionRepository.GetFieldAsync("refresh_token", refresh_token);
            if (Utils.IsEmpty(usersSession))
            {
                rs = new ServiceResultError("This information does not exist");
                return rs;
            }
            await _usersSessionRepository.DeleteAsync(usersSession.ID, ID);
            rs = new ServiceResultSuccess("Logout successful");
            return rs;
        }
        #endregion
        #region GetInfo
        public async Task<ServiceResult> GetInfo(long ID)
        {
            var rs = new ServiceResult();
            var user = await _usersRepository.GetAsync(ID);
            if (Utils.IsEmpty(user))
            {
                rs = new ServiceResultError("This information does not exist");
                return rs;
            }
            else
            {
                var userDTO = _mapper.Map<UsersDTO>(user);
                var data = new
                {
                    UserName = userDTO.UserName,
                    FullName = userDTO.FullName,
                    user.Avatar,
                    user.Email,
                    user.Phone,
                    user.Address,
                    user.Gender,
					userDTO.IsAdmin
                };
                rs = new ServiceResultSuccess(data);
                return rs;
            }
        }
        #endregion
        #region RefreshToken
        public async Task<ServiceResult> RefreshToken(LogoutDTO model)
        {
            var rs = new ServiceResult();
            var usersSession = await _usersSessionRepository.GetFieldAsync("refresh_token", model.RefreshToken);
            if (Utils.IsEmpty(usersSession))
            {
                rs = new ServiceResultError("Token not found.");
                return rs;
            }
            var usersSessionDTO = _mapper.Map<UsersSessionDTO>(usersSession);
            if (usersSessionDTO.IsExpired)
            {
                // Remove refresh token if expired
                await _usersSessionRepository.DeleteAsync(usersSession.ID, usersSession.CreatedBy.Value);
                rs = new ServiceResultError("Token has expired.");
                return rs;
            }
            //remove the old refresh_token and add a new refresh_token
            await _usersSessionRepository.DeleteAsync(usersSession.ID, usersSession.CreatedBy.Value);
            // Tạo sesion mỗi lần khi đăng nhập
            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");
            var user = await _usersRepository.GetAsync(usersSession.UserId);
            var userDTO = _mapper.Map<UsersDTO>(user);
            await CreateUserSession(userDTO, usersSession.RememberMe, refresh_token);
            // tạo token
            var response = GenerateJwtToken(userDTO, usersSession.RememberMe, refresh_token);
            rs = new ServiceResultSuccess("Refresh token successful", response);
            return rs;


        }
        #endregion
        #region ChangePassword
        public async Task<ServiceResult> ChangePassword(ChangePasswordDTO model, long id)
        {
            var rs = new ServiceResult();
            var user = await _usersRepository.GetAsync(id);
            if (Utils.IsEmpty(user))
            {
                rs = new ServiceResultError("This information does not exist");
                return rs;
            }
            else
            {
                user.PasswordHash = Hash.GetPasswordHash(model.NewPassword);
                await _usersRepository.UpdateAsync(user);
                rs = new ServiceResultSuccess("Password changed successfully");
                return rs;
            }
        }
        #endregion

        #region UpdateProfile
        public async Task<ServiceResult> UpdateProfile(UpdateProfileDTO model, long id)
        {
            var entity = await _usersRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            if (await _usersRepository.EmailExists(model.Email, id))
            {
                return new ServiceResultError("This email already exists");
            }
            var entityUpdate = Other.BindUpdate<Users>(entity, model, id);
            var data = await _usersRepository.UpdateAsync(entityUpdate);
            return new ServiceResultSuccess("Update profile successfully");
        }
        #endregion

        #region Function
        private async Task<bool> CreateUserInitialize()
        {
            var _usersCreate = new UsersDTOCreate
            {
                FullName = "Quản trị hệ thống",
                UserName = "admin",
                IsAdmin = true,
                Parent = 0,
                Email = "daidc@sunhouse.com.vn",
                Address = "35 Mạc Thái Tổ, Yên Hòa, Cầu Giấy, Hà Nội",
                CreatedBy = 1,
                Gender = 1,
                Phone = "18006680",
                Status = (int)EnumCommon.StatusUsers.Active,
            };
            var entity = _mapper.Map<Users>(_usersCreate);
            entity.PasswordHash = Hash.GetPasswordHash("abc@1234");
            await _usersRepository.InsertAsync(entity);
            return true;
        }
        private async Task<bool> CreateUserSession(UsersDTO user, bool RememberMe, string refresh_token)
        {
            var ExpiresTimeToken = _settings.Value.ExpiresTimeToken;
            var _usersSessionCreate = new UsersSessionDTOCreate
            {
                UserId = user.ID,
                UserName = user.UserName,
                RefreshToken = refresh_token,
                IdentityRefreshTokenId = Guid.NewGuid().ToString(),
                IssuedTime = DateTime.UtcNow,
                ExpireTime = DateTime.UtcNow.AddDays(RememberMe ? ExpiresTimeToken : 1),
                CreatedBy = user.ID,
                RememberMe = RememberMe,
            };
            await _usersSessionRepository.InsertAsync(_mapper.Map<UsersSession>(_usersSessionCreate));
            return true;
        }
        private object GenerateJwtToken(UsersDTO user, bool RememberMe, string refresh_token)
        {
            var ExpiresTimeToken = _settings.Value.ExpiresTimeToken;
            var now = DateTime.UtcNow;
            var claims = new Claim[]
           {
                new Claim("UserId",user.ID.ToString()),
                new Claim("Parent",user.Parent.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("RefreshToken", refresh_token),
                new Claim("IsAdministrator", user.IsAdmin?"true":"false"),
                new Claim("FullName", user.FullName)
           };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Secret));
            var expires = now.Add(TimeSpan.FromDays(RememberMe ? ExpiresTimeToken : 1));
            var jwt = new JwtSecurityToken(
                issuer: _settings.Value.ValidIssuer,
                audience: _settings.Value.ValidAudience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires,
                refresh_token,
                fullName = user.FullName,
                username = user.UserName,
            };
            return response;
        }
        #endregion
        #region Gửi yêu cầu
        public async Task<ServiceResult> SendRequestAsync(UsersRequestDTO model)
        {
            var Country = model.Country;

			var dattr = Utils.DateToString(model.RequestDate);
            model.Country = Utils.IsNotEmpty(model.Country) ? model.Country.ToLower().Trim() : "en";
            model.Country = model.Country == "vn" ? "vi" : model.Country;
            if (model.AdminPassword.Length > 255 || model.AdminPassword.Length < 6)
            {
                return new ServiceResultError("Password must be between 6 and 255 characters");
            }
            var userlogin = await _usersRepository.GetFieldAsync("user_name", model.AdminUser);
            if (userlogin == null || userlogin.ID <= 0)//Nếu không tìm thấy theo tên đăng nhập thì tìm user theo email
            {
                userlogin = await _usersRepository.GetFieldAsync("email", model.AdminUser);
            }
            if (Utils.IsEmpty(userlogin)) //Đăng nhập sai & hoặc account không tồn tại
            {
                return new ServiceResultError("Account information is incorrect");
            }
            if (!userlogin.IsAdmin)
            {
                return new ServiceResultError("You do not have permission to create a user");
            }

            var replacements = new Dictionary<string, string>
                {
                    {"{RequestNumber}",model.RequestNumber },
                    {"{Supplier}",model.Name },
                    {"{PONumber}",model.PONumber },
                    {"{CreateBy}",model.CreateBy },
                    {"{RequestDate}", Utils.DateToString(model.RequestDate) },
                    {"{CompletionDate}",Utils.DateToString(model.RequestDate.AddDays(3)) },
                    {"[EMAIL_URL_LINK]",_configuration["URLQC:Portal"] },
                };
            
            var pass = GenerateRandomString(8);
            var user = new Users
            {
                Email = model.Email.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim(),
                UserName = model.UserName,
                FullName = model.Name,
                Phone = model.Phone,
                Address = model.Address,
                PasswordHash = Hash.GetPasswordHash(pass),
                Parent = 0,
                Gender = 0,
                Status = (int)EnumCommon.StatusUsers.Active,
                CreatedBy = userlogin.ID,
                CreatedDate = DateTime.UtcNow,
			};
            var data = await _usersRepository.InsertAsync(user);
            
            return new ServiceResultSuccess("Record added successfully", _mapper.Map<UsersDTOCreate>(data));
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

    }
}
