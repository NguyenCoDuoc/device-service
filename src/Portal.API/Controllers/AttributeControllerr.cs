using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using DeviceService.Common.Controllers;


namespace DeviceService.API.Controllers
{
    [ApiController]
    [Route("api/Attribute")]
    [EnableCors("CorsApi")]
    public class AttributeController : BaseController
    {
        private readonly IAttributeService _AttributeService;

        public AttributeController(IAttributeService AttributeService , IAccountServices accountServices)
        {
            _AttributeService = AttributeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLists()
        {
            var searchParam = new SearchParam
            {
                ItemsPerPage = -1,
                SortBy = "created_date",
                SortDesc = false,
            };
            return Ok(await _AttributeService.GetPagingAsync(searchParam));
        }

        /// <summary>
        /// Lấy danh sách thuộc tính
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        private async Task<PagingResult<AttributeDto>> GetListsData(SearchParam searchParam)
        {
            return await _AttributeService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết thuộc tính
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return JSonResult(await _AttributeService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới thuộc tính
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeDtoCreate model)
        {
            return new JsonResult(await _AttributeService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật thuộc tính
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttributeDto model)
        {
            return Ok(await _AttributeService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa thuộc tính
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _AttributeService.DeleteAsync(id));
        }

        /// <summary>
        /// Lấy danh sách thuộc tính
        /// </summary>
        /// DUOCNC 20240812
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _AttributeService.GetAllAsync());
        }

    }
}
