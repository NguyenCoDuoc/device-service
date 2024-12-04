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
    [Route("api/Location")]
    [EnableCors("CorsApi")]
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;


        public LocationController(ILocationService LocationService)
        {
            _locationService = LocationService;
        }

        /// <summary>
        /// Lấy danh sách vị trí phân trang
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
        /// Lấy danh sách vị trí
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        private async Task<PagingResult<LocationDto>> GetListsData(SearchParam searchParam)
        {
            return await _locationService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết vị trí
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return JSonResult(await _locationService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới vị trí
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocationDtoCreate model)
        {
            return new JsonResult(await _locationService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật vị trí
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LocationDto model)
        {
            return Ok(await _locationService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa vị trí
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _locationService.DeleteAsync(id));
        }

        /// <summary>
        /// Lấy danh all sách phòng ban
        /// </summary>
        /// DUOCNC 20240812
        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<LocationDtoDetail>> GetAllLocation()
        {
            return await _locationService.GetAllAsync();
        }
    }
}
