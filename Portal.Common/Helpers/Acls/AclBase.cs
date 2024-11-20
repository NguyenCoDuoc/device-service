using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DeviceService.Domain.Interfaces;
using Sun.Core.Share.Constants;
using Sun.Core.Share.Helpers.Util;
using System.Net;
using DeviceService.Application.ContextAccessors;
using static DeviceService.Application.Helpers.Enums.EnumCommon;


namespace DeviceService.Common.Helpers.Acls
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AclBase : Attribute
    {
        protected bool IsRemember { get; set; } = false;
        protected  bool IsAuthorized(AuthorizationFilterContext filterContext)
        {
            IServiceProvider services = filterContext.HttpContext.RequestServices;
            var account = (IUserPrincipalService)services.GetService(typeof(IUserPrincipalService));
            var _usersSessionRepository = (IUsersSessionRepository)services.GetService(typeof(IUsersSessionRepository));
            var _usersRepository = (IUsersRepository)services.GetService(typeof(IUsersRepository));
            if (Utils.IsNotEmpty(account))
            {
                var usersSession = _usersSessionRepository.GetFieldAsync("refresh_token", account.RefreshToken).Result;
                if (Utils.IsEmpty(usersSession))
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    filterContext.Result = new JsonResult(new
                    {
                        Code = 403,
                        Type = CommonConst.Error,
                        Message = "Invalid credentials"
                    });
                    return false;
                }
                if (DateTime.UtcNow >= usersSession.ExpireTime)
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    filterContext.Result = new JsonResult(new
                    {
                        Code = 403,
                        Type = CommonConst.Error,
                        Message = "Credentials have expired"
                    });
                    return false;
                }
                var users = _usersRepository.GetAsync(account.UserId).Result;
                if (users.Status == (int)StatusUsers.Locked)
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    filterContext.Result = new JsonResult(new
                    {
                        Code = 403,
                        Type = CommonConst.Error,
                        Message = "Your account is disabled"
                    });
                    return false;
                }
                return true;
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                filterContext.Result = new JsonResult(new
                {
                    Code = 417,
                    Type = CommonConst.Error,
                    Message = "Information is unverified"
                });
                return false;
            }
        }
    }

}

