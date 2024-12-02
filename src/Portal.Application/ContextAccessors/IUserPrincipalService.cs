using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Application.ContextAccessors
{
    public interface IUserPrincipalService
    {
        long UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
        List<string> Privileges { get; }
        string RefreshToken { get; }
        bool IsAdministrator { get; }
        string FullName { get; }
        long Parent { get; }
    }
}
