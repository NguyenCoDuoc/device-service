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
    [Route("api/Serial")]
    [EnableCors("CorsApi")]
    public class SerialController : BaseController
    {
        private readonly ISerialService _SerialService;
        private readonly IJsActionResult _result;


        public SerialController(ISerialService SerialService, IJsActionResult sunactionresult)
        {
            _SerialService = SerialService;
            _result = sunactionresult;
        }

        /// <summary>
        /// Lấy danh sách Serial phân trang
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
        /// Lấy danh sách Serial
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        private async Task<PagingResult<SerialDto>> GetListsData(SearchParam searchParam)
        {
            return await _SerialService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết Serial
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return _result.JsonResult(await _SerialService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới Serial
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SerialDtoCreate model)
        {
            return new JsonResult(await _SerialService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật Serial
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SerialDto model)
        {
            return Ok(await _SerialService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa Serial
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _SerialService.DeleteAsync(id));
        }
    }
}
