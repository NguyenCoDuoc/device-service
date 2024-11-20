using Microsoft.AspNetCore.Mvc;
using Portal.Application.ContextAccessors;
using Portal.Application.Interfaces;
using Portal.Common.Controllers;
using Sun.Core.Share.Helpers.Params;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.API.Controllers
{
    [Route("api/qc-inspection-group")]
    [ApiController]
    public class QCInspectionGroupController : BaseAuthorizeController
    {
        #region Properties
        private readonly IQCInspectionGroupServices _qcInspectionGroupService;
        private readonly IUserPrincipalService _CurrentUser;
        private readonly IConfiguration _configuration;
        #endregion
        #region Ctor
        public QCInspectionGroupController(IConfiguration configuration
        , IQCInspectionGroupServices qcInspectionGroupService, IUserPrincipalService userIdentity)
        {
            _configuration = configuration;
            _qcInspectionGroupService = qcInspectionGroupService;
            _CurrentUser= userIdentity;
        }
		#endregion
		[SwaggerOperation(Summary = "Danh sách nhóm tiêu chí")]
		[HttpGet]
        public async Task<IActionResult> GetLists([FromQuery] SearchParam searchParam)
        {
            return Ok(await _qcInspectionGroupService.GetPagingAsync(searchParam));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCode(string id)
        {
            return JSonResult(await _qcInspectionGroupService.GetByCodeAsync(id));
        }
       

    }
}
