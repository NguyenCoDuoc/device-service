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
    [Route("api/serial-location")]
    [EnableCors("CorsApi")]
    public class SerialLocationController : BaseController
    {
        private readonly ISerialLocationService _SerialLocationService;


        public SerialLocationController(ISerialLocationService SerialLocationService)
        {
            _SerialLocationService = SerialLocationService;
        }

        /// <summary>
        /// Lấy danh sách vị trí serial phân trang
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
        /// Lấy danh sách vị trí serial
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        private async Task<PagingResult<SerialLocationDto>> GetListsData(SearchParam searchParam)
        {
            return await _SerialLocationService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết vị trí serial
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return JSonResult(await _SerialLocationService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới vị trí serial
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SerialLocationDtoCreate model)
        {
            return new JsonResult(await _SerialLocationService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật vị trí serial
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SerialLocationDto model)
        {
            return Ok(await _SerialLocationService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa vị trí serial
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _SerialLocationService.DeleteAsync(id));
        }
    }
}
