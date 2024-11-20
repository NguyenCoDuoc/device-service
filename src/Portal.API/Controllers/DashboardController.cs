using Microsoft.AspNetCore.Mvc;
using Portal.Application.ContextAccessors;
using Portal.Application.Interfaces;
using Portal.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseAuthorizeController
    {
        #region Properties
        private readonly IUserPrincipalService _currentUser;
        private readonly IInspectionDashboardServices _inspectionDashboardServices;

        #endregion
        #region Ctor
        public DashboardController(IConfiguration configuration
            , IInspectionDashboardServices inspectionDashboardServices
            , IUserPrincipalService userIdentity)
        {
            _inspectionDashboardServices = inspectionDashboardServices;
            _currentUser = userIdentity;
        }
        #endregion
        //// <summary>
        //// GetInfo
        //// </summary>
        //// <returns>Token</returns>  
        [SwaggerOperation(Summary = "Thống kê theo trạng thái")]
        [HttpGet]
        [Route("get-statistic-status")]
        public async Task<IActionResult> GetStatisticsStatus(int Month = 0, int Year = 0,bool isViewRequest = false)
        {
            return JSonResult(await _inspectionDashboardServices.GetStaticStatus(_currentUser.cmp_wwn,Month,Year, isViewRequest));
        }
    }
}
