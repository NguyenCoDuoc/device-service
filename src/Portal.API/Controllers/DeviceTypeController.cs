using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sun.Core.BaseServiceCollection.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using System.Security.Claims;
using DeviceService.Common.Controllers;

namespace DeviceService.API.Controllers
{
    [ApiController]
    [Route("api/device-type")]
    [EnableCors("CorsApi")]
    public class DeviceTypeController : BaseController
    {
        private readonly IDeviceTypeService _DeviceTypeService;

        public DeviceTypeController(IDeviceTypeService DeviceTypeService)
        {
            _DeviceTypeService = DeviceTypeService;
        }

        /// <summary>
        /// Lấy danh sách loại thiết bị phân trang
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
        /// Lấy danh sách loại thiết bị
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240916
        private async Task<PagingResult<DeviceTypeDto>> GetListsData(SearchParam searchParam)
        {
            return await _DeviceTypeService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết loại thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return new JsonResult(await _DeviceTypeService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới loại thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceTypeDtoCreate model)
        {
            return new JsonResult(await _DeviceTypeService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật loại thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeviceTypeDto model)
        {
            return Ok(await _DeviceTypeService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa loại thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _DeviceTypeService.DeleteAsync(id));
        }

        /// <summary>
        /// Lấy danh all sách loại thiết bị
        /// </summary>
        /// DUOCNC 20240916
        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<DeviceTypeDtoDetail>> GetAllDeviceType()
        {
            return await _DeviceTypeService.GetAllAsync();
        }

        /// <summary>
        /// Get device type attributes by device type id
        /// </summary>
        /// <param name="deviceTypeId">Device type id</param>
        /// <returns>List of device type attributes</returns>
        [HttpGet("{deviceTypeId}/attributes")]
        public async Task<IEnumerable<DeviceTypeAttributeDto>> GetDeviceTypeAttributes(string deviceTypeId)
        {
            if (!long.TryParse(deviceTypeId, out var id))
            {
                return new List<DeviceTypeAttributeDto>() ;
            }

            return await _DeviceTypeService.GetDeviceTypeAttributes(id);
        }

        /// <summary>
        /// Add device type attribute
        /// </summary>
        /// <param name="deviceTypeAttribute">Device type attribute</param>
        /// <returns>Device type attribute id</returns>
        /// DUOCNC 20241106
        [HttpPost("{deviceTypeId}/attributes")]
        public async Task<IActionResult> AddDeviceTypeAttribute(DeviceTypeAttributeDtoCreate deviceTypeAttribute)
        {
            return Ok(await _DeviceTypeService.AddDeviceTypeAttribute(deviceTypeAttribute));
        }

        /// <summary>
        /// Delete device type attribute
        /// </summary>
        /// <param name="id">Device type attribute id</param>
        /// DUOCNC 20241106
        [HttpDelete("/attributes/{id}")]
        public async Task<IActionResult> DeleteDeviceTypeAttribute(long id)
        {
            return Ok(await _DeviceTypeService.DeleteDeviceTypeAttribute(id));
        }
    }
}
