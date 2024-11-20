using Microsoft.AspNetCore.Mvc;
using Portal.Application.ContextAccessors;
using Portal.Application.Interfaces;
using Portal.Common.Controllers;
using Sun.Core.Share.Helpers.Params;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.API.Controllers
{
    [Route("api/qc-inspection-item-code")]
    [ApiController]
    public class QCInspectionItemCodeController : BaseAuthorizeController
    {
        #region Properties
        private readonly IQCInspectionItemCodeServices _qcInspectionItemCodeService;
        private readonly IUserPrincipalService _CurrentUser;
        private readonly IConfiguration _configuration;
        #endregion
        #region Ctor
        public QCInspectionItemCodeController(IConfiguration configuration
        , IQCInspectionItemCodeServices qcInspectionItemCodeService, IUserPrincipalService userIdentity)
        {
            _configuration = configuration;
            _qcInspectionItemCodeService = qcInspectionItemCodeService;
            _CurrentUser= userIdentity;
        }
		#endregion
		[SwaggerOperation(Summary = "Danh sách tiêu chí kiểm hàng theo mã hàng")]
		[HttpGet]
        public async Task<IActionResult> GetLists([FromQuery] SearchParam searchParam)
        {
            return Ok(await _qcInspectionItemCodeService.GetPagingAsync(searchParam));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return JSonResult(await _qcInspectionItemCodeService.GetByIdAsync(id));
        }
       
    }
}
