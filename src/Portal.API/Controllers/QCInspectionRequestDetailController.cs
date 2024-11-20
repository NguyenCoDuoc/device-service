using Microsoft.AspNetCore.Mvc;
using Portal.Application.ContextAccessors;
using Portal.Application.DTOS.QCInspectionRequest;
using Portal.Application.Interfaces;
using Portal.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.API.Controllers
{
    [Route("api/qc-inspection-request-detail")]
	[ApiController]
	public class QCInspectionRequestDetailController : BaseAuthorizeController
	{
		#region Properties
		private readonly IQCInspectionRequestDetailServices _requestServices;
		private readonly IUserPrincipalService _CurrentUser;
		private readonly IConfiguration _configuration;
		#endregion
		#region Ctor
		public QCInspectionRequestDetailController(IConfiguration configuration
		, IQCInspectionRequestDetailServices requestServices, IUserPrincipalService userIdentity)
		{
			_configuration = configuration;
			_requestServices = requestServices;
			_CurrentUser = userIdentity;
		}
		#endregion
		[SwaggerOperation(Summary = "Danh sách kiểm thử")]
		[HttpGet]
		public async Task<IActionResult> GetLists([FromQuery] QCInspectionRequestSearchParam searchParam)
		{
			return Ok(await _requestServices.GetPagingAsync(searchParam));
		}
		[HttpGet("get-by-itemcode/{ItemCode}/{LotSize}")]
		public async Task<IActionResult> GetByItem(string ItemCode, double LotSize)
		{
			return JSonResult(await _requestServices.GetByItem(ItemCode, LotSize));
		}
		[SwaggerOperation(Summary = "Tạo và cập nhật kết quả kiểm thử")]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] QCInspectionRequestSaveDTO model)
		{
			return JSonResult(await _requestServices.CreateAsync(model.detail, model.detailResults));
		}
		//[SwaggerOperation(Summary = "Chấp nhận phiếu kiểm thử và tạo mã phiếu")]
		//[HttpPatch("accept-status/{id}")]
		//public async Task<IActionResult> AcceptStatus(string id)
		//{
		//	return JSonResult(await _requestServices.UpdateStatusAsync(id, "PROCESSING"));
		//}
        [SwaggerOperation(Summary = "Cập nhật trạng thái chi tiết phiếu kiếm")]
        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] QCInspectionRequestStatusDTO model)
        {
            return JSonResult(await _requestServices.UpdateStatusAsync(model.Id, model.Status));
        }
        [SwaggerOperation(Summary = "Tạo mới mã phiếu và tạo mã phiếu khi bị từ chối")]
		[HttpPost("clone-detail/{id}")]
		public async Task<IActionResult> CloneDetail(string id)
		{
			return JSonResult(await _requestServices.CloneAsync(id));
		}
        [SwaggerOperation(Summary = "Tạo và cập nhật kết quả kiểm thử")]
        [HttpPut("update-result")]
        public async Task<IActionResult> UpdateResult([FromBody] QCInspectionRequestDetailResultDTOUpdate model)
        {
            return JSonResult(await _requestServices.UpdateDetailResultAsync(model));
        }
    }
}
