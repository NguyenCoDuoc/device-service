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
    [Route("api/department")]
    [EnableCors("CorsApi")]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;


        public DepartmentController(IDepartmentService DepartmentService)
        {
            _departmentService = DepartmentService;
        }

        /// <summary>
        /// Lấy danh sách phòng ban phân trang
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
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <param name="searchParam"></param>
        /// DUOCNC 20240812
        private async Task<PagingResult<DepartmentDto>> GetListsData(SearchParam searchParam)
        {
            return await _departmentService.GetPagingAsync(searchParam);
        }
        
        /// <summary>
        /// Lấy thông tin chi tiết phòng ban
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return JSonResult(await _departmentService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDtoCreate model)
        {
            return new JsonResult(await _departmentService.CreateAsync(model));
        }
        
        /// <summary>
        /// Cập nhật phòng ban
        /// </summary>
        /// <param name="model"></param>
        /// DUOCNC 20240812
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DepartmentDto model)
        {
            return Ok(await _departmentService.UpdateAsync(model));
        }
        
        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="id"></param>
        /// DUOCNC 20240812
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _departmentService.DeleteAsync(id));
        }

        /// <summary>
        /// Lấy danh all sách phòng ban
        /// </summary>
        /// DUOCNC 20240812
        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<DepartmentDtoDetail>> GetAllDepartment()
        {
            return await _departmentService.GetAllAsync();
        }

        /// <summary>
        /// Lấy danh all sách phòng ban theo user id
        /// </summary>
        /// DUOCNC 20240812
        [HttpGet]
        [Route("department_by_user/{userId}")]
        public async Task<IEnumerable<DepartmentDtoDetail>> GetDepartmentsByUserId(long userId)
        {
            return await _departmentService.GetDepartmentsByUserId(userId);
        }
    }
}
