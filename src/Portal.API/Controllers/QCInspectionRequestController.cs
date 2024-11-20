using Microsoft.AspNetCore.Mvc;
using Portal.Application.ContextAccessors;
using Portal.Application.DTOS.QCInspectionRequest;
using Portal.Application.Interfaces;
using Portal.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.API.Controllers
{
	[Route("api/qc-inspection-request")]
	[ApiController]
	public class QCInspectionRequestController : BaseAuthorizeController
	{
		#region Properties
		private readonly IQCInspectionRequestServices _requestServices;
		private readonly IUserPrincipalService _CurrentUser;
		private readonly IConfiguration _configuration;
		#endregion
		#region Ctor
		public QCInspectionRequestController(IConfiguration configuration
		, IQCInspectionRequestServices requestServices, IUserPrincipalService userIdentity)
		{
			_configuration = configuration;
			_requestServices = requestServices;
			_CurrentUser = userIdentity;
		}
		#endregion
		[SwaggerOperation(Summary = "Danh sách yêu cầu")]
		[HttpGet]
		public async Task<IActionResult> GetLists([FromQuery] QCInspectionRequestSearchParam searchParam)
		{
			return Ok(await _requestServices.GetPagingAsync(searchParam));
		}
		[SwaggerOperation(Summary = "Chi tiết của 1 yêu cầu và danh sách kiểm thử")]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			return JSonResult(await _requestServices.GetByIdAsync(id));
		}
		[SwaggerOperation(Summary = "Chi tiết của 1 kiểm thử và danh sách kết quả")]
		[HttpGet("{id}/{detailid}")]
		public async Task<IActionResult> GetById(string id, string detailid)
		{
			return JSonResult(await _requestServices.GetByIdAsync(id, detailid));
		}
		[SwaggerOperation(Summary = "Cập nhật trạng thái phiếu yêu cầu")]
		[HttpPatch("update-status")]
		public async Task<IActionResult> UpdateStatus([FromBody] QCInspectionRequestStatusDTO model)
		{
			return JSonResult(await _requestServices.UpdateStatusAsync(model));
		}
	}
}
