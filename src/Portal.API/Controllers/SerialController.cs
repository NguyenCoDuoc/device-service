using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using DeviceService.Common.Controllers;
using DeviceService.Application.Services;

namespace DeviceService.API.Controllers
{
    [ApiController]
    [Route("api/Serial")]
    [EnableCors("CorsApi")]
    public class SerialController : BaseController
    {
        private readonly ISerialService _SerialService;


        public SerialController(ISerialService SerialService)
        {
            _SerialService = SerialService;
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
            return JSonResult(await _SerialService.GetByIdAsync(id));
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

        [HttpGet("{serialId}/attributes")]
        public async Task<IEnumerable<SerialAttributeDto>> GetSerialAttributes(string serialId)
        {
            if (!long.TryParse(serialId, out var id))
            {
                return new List<SerialAttributeDto>();
            }

            return await _SerialService.GetSerialAttributes(id);
        }

        [HttpPost("{serialId}/attributes")]
        public async Task<IActionResult> AddSerialAttribute(SerialAttributeDto serialAttributeDto)
        {
            return Ok(await _SerialService.AddSerialAttribute(serialAttributeDto));
        }


        [HttpDelete("attributes/{id}")]
        public async Task<IActionResult> DeleteSerialAttribute(long id)
        {
            return Ok(await _SerialService.DeleteSerialAttribute(id));
        }
    }
}
