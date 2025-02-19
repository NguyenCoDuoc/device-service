﻿using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using DeviceService.Common.Controllers;
using DeviceService.Application.Services;

namespace DeviceService.API.Controllers
{
    [ApiController]
    [Route("api/device")]
    [EnableCors("CorsApi")]
    public class DeviceController : BaseController
    {
        private readonly IDeviceService _DeviceService;
        public DeviceController(IDeviceService DeviceService)
        {
            _DeviceService = DeviceService;
        }

        /// <summary>
        /// Lấy danh sách thiết bị phân trang
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
        /// Lấy danh sách thiết bị
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240916
        private async Task<PagingResult<DeviceDto>> GetListsData(SearchParam searchParam)
        {
            return await _DeviceService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return new JsonResult(await _DeviceService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceDtoCreate model)
        {
            return new JsonResult(await _DeviceService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật thiết bị
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240916
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeviceDto model)
        {
            return Ok(await _DeviceService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa thiết bị
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240916
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _DeviceService.DeleteAsync(id));
        }

        /// <summary>
        /// Lấy danh all sách thiết bị
        /// </summary>
        /// DUOCNC 20240916
        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<DeviceDto>> GetAllDevice()
        {
            return await _DeviceService.GetAllAsync();
        }


        /// <summary>
        /// GetDeviceAttributes
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpGet("{deviceId}/attributes")]
        public async Task<IEnumerable<DeviceAttributeDto>> GetDeviceAttributes(string deviceId)
        {
            if (!long.TryParse(deviceId, out var id))
            {
                return new List<DeviceAttributeDto>();
            }

            return await _DeviceService.GetDeviceAttributes(id);
        }

        /// <summary>
        /// AddDeviceAttribute
        /// </summary>
        /// <param name="deviceAttribute"></param>
        /// <returns></returns>
        [HttpPost("{deviceId}/attributes")]
        public async Task<IActionResult> AddDeviceAttribute(DeviceAttributeDtoCreate deviceAttribute)
        {
            return Ok(await _DeviceService.AddDeviceAttribute(deviceAttribute));
        }

        /// <summary>
        /// DeleteDeviceAttribute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("attributes/{id}")]
        public async Task<IActionResult> DeleteDeviceAttribute(long id)
        {
            return Ok(await _DeviceService.DeleteDeviceAttribute(id));
        }
    }
}
