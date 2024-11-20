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
    [Route("api/device-unit")]
    [EnableCors("CorsApi")]
    public class DeviceUnitController : BaseAuthorizeController
    {
        private readonly IDeviceUnitService _DeviceUnitService;


        public DeviceUnitController(IDeviceUnitService DeviceUnitService)
        {
            _DeviceUnitService = DeviceUnitService;
        }

        /// <summary>
        /// Lấy danh sách đơn vị tính phân trang
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240916
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery] SearchParam searchParam)
        {
            var data = await GetListsData(searchParam);
            return Ok(data);
        }
        
        /// <summary>
        /// Lấy danh sách đơn vị tính
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240916
        private async Task<PagingResult<DeviceUnitDto>> GetListsData(SearchParam searchParam)
        {
            return await _DeviceUnitService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Thêm mới đơn vị tính
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceUnitDtoCreate model)
        {
            return new JsonResult(await _DeviceUnitService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật đơn vị tính
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeviceUnitDto model)
        {
            return Ok(await _DeviceUnitService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa đơn vị tính
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _DeviceUnitService.DeleteAsync(id));
        }

        /// <summary>
        /// Lấy danh all sách đơn vị tính
        /// </summary>
        /// DUOCNC 20240916
        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<DeviceUnitDtoDetail>> GetAllDeviceUnit()
        {
            return await _DeviceUnitService.GetAllAsync();
        }
    }
}
