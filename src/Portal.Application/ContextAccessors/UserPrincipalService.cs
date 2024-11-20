using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Application.ContextAccessors
{
    public class UserPrincipalService : IUserPrincipalService
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        public UserPrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            _claimsPrincipal = httpContextAccessor.HttpContext?.User;
        }
        public bool IsAuthenticated => _claimsPrincipal.Identity.IsAuthenticated; // Kiểm tra xác thực
        private string GetClaimValue(string claimType)
        {
            var claim = _claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim == null ? "" : claim.Value;
        }
        private List<string> GetPrivileges(string claimType)
        {
            List<string> lstPrivileges = new List<string>();
            var claim = _claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                lstPrivileges = claim.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return lstPrivileges;
        }
        public long UserId { get => Convert.ToInt32(GetClaimValue("UserId")); }
        public string UserName { get => GetClaimValue("UserName"); }
        public List<string> Privileges { get => GetPrivileges("Privileges"); }
        public bool IsAdministrator
        {
            get => Convert.ToBoolean(GetClaimValue("IsAdministrator"));
        }
        public string FullName { get => GetClaimValue("FullName"); }
        public string RefreshToken { get => GetClaimValue("RefreshToken"); }
        public long Parent { get => Convert.ToInt32(GetClaimValue("Parent")); }
        public string cmp_wwn { get => GetClaimValue("cmp_wwn"); }

    }
}
