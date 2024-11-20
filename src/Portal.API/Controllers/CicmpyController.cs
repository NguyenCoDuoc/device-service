using Microsoft.AspNetCore.Mvc;
using Portal.Application.ContextAccessors;
using Portal.Application.Interfaces;
using Portal.Common.Controllers;
using Sun.Core.Share.Helpers.Params;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.API.Controllers
{
	
	[Route("api/Cicmpy")]
	[ApiController]
	public class CicmpyController : BaseAuthorizeController
	{
		#region Properties
		private readonly ICicmpyConsolidatedServices _cicmpyConsolidatedService;
		private readonly IUserPrincipalService _CurrentUser;
		private readonly IConfiguration _configuration;
		#endregion
		#region Ctor
		public CicmpyController(IConfiguration configuration
		, ICicmpyConsolidatedServices cicmpyConsolidatedService, IUserPrincipalService userIdentity)
		{
			_configuration = configuration;
			_cicmpyConsolidatedService = cicmpyConsolidatedService;
			_CurrentUser = userIdentity;
		}
		#endregion
		[SwaggerOperation(Summary = "Danh sách nhà cung cấp")]
		[HttpGet]
		public async Task<IActionResult> GetLists([FromQuery] SearchParam searchParam)
		{
			return Ok(await _cicmpyConsolidatedService.GetPagingAsync(searchParam));
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			return JSonResult(await _cicmpyConsolidatedService.GetByIdAsync(id));
		}


	}
}
