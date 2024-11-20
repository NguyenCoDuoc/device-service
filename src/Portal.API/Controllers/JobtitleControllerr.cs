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
    [Route("api/Jobtitle")]
    [EnableCors("CorsApi")]
    public class JobtitleController : BaseController
    {
        private readonly IJobtitleService _JobtitleService;
        private readonly IJsActionResult _result;


        public JobtitleController(IJobtitleService JobtitleService, IJsActionResult sunactionresult)
        {
            _JobtitleService = JobtitleService;
            _result = sunactionresult;
        }

        /// <summary>
        /// Lấy danh sách chức danh phân trang
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
        /// Lấy danh sách chức danh
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        private async Task<PagingResult<JobtitleDto>> GetListsData(SearchParam searchParam)
        {
            return await _JobtitleService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết chức danh
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return _result.JsonResult(await _JobtitleService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới chức danh
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobtitleDtoCreate model)
        {
            return new JsonResult(await _JobtitleService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật chức danh
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] JobtitleDto model)
        {
            return Ok(await _JobtitleService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa chức danh
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _JobtitleService.DeleteAsync(id));
        }
    }
}
