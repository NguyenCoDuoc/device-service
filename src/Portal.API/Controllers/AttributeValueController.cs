using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sun.Core.BaseServiceCollection.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using DeviceService.Common.Controllers;


namespace DeviceService.API.Controllers
{
    [ApiController]
    [Route("api/attribute-value")]
    [EnableCors("CorsApi")]
    public class AttributeValueController : BaseController
    {
        private readonly IAttributeValueService _AttributeValueService;
        private readonly IJsActionResult _result;


        public AttributeValueController(IAttributeValueService AttributeValueService, IJsActionResult sunactionresult)
        {
            _AttributeValueService = AttributeValueService;
            _result = sunactionresult;
        }

        /// <summary>
        /// Lấy danh sách Thuộc tính giá trị phân trang
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery] SearchParam searchParam)
        {
            var data = await GetListsData(searchParam);
            return Ok(data);
        }
        
        /// <summary>
        /// Lấy danh sách Thuộc tính giá trị
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        private async Task<PagingResult<AttributeValueDto>> GetListsData(SearchParam searchParam)
        {
            return await _AttributeValueService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết Thuộc tính giá trị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return _result.JsonResult(await _AttributeValueService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới Thuộc tính giá trị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeValueDtoCreate model)
        {
            return new JsonResult(await _AttributeValueService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật Thuộc tính giá trị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttributeValueDto model)
        {
            return Ok(await _AttributeValueService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa Thuộc tính giá trị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _AttributeValueService.DeleteAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _AttributeValueService.GetAllAsync());
        }
    }
}
