using Microsoft.AspNetCore.Mvc;
using Portal.Application.ContextAccessors;
using Portal.Application.Interfaces;
using Portal.Common.Controllers;
using Sun.Core.Share.Helpers.Params;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.API.Controllers
{
    [Route("api/qc-aql-data-sheet")]
    [ApiController]
    public class QCAQLDataSheetController : BaseAuthorizeController
    {
        #region Properties
        private readonly IQCAQLDataSheetServices _qCAQLDataSheetServices;
        private readonly IUserPrincipalService _CurrentUser;
        private readonly IConfiguration _configuration;
        #endregion
        #region Ctor
        public QCAQLDataSheetController(IConfiguration configuration
        , IQCAQLDataSheetServices qCAQLDataSheetServices, IUserPrincipalService userIdentity)
        {
            _configuration = configuration;
            _qCAQLDataSheetServices = qCAQLDataSheetServices;
            _CurrentUser = userIdentity;
        }
        #endregion
        [SwaggerOperation(Summary = "Danh sách ")]
        [HttpGet]
        public async Task<IActionResult> GetLists()
        {
            SearchParam searchParam = new SearchParam
            {
                Page=-1 ,
                ItemsPerPage=-1,
            };
            return Ok(await _qCAQLDataSheetServices.GetPagingAsync(searchParam));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return JSonResult(await _qCAQLDataSheetServices.GetByIdAsync(id));
        }


    }
    
}
