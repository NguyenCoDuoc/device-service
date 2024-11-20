using Microsoft.AspNetCore.Mvc.Filters;

namespace DeviceService.Common.Helpers.Acls
{
    public class AclAuthorize : AclBase, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IsAuthorized(context);
        }
    }
}
