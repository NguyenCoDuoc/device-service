using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using DeviceService.Common.Controllers;
using DeviceService.Application.DTOS.Users;


namespace DeviceService.API.Controllers
{
    [ApiController]
    [Route("api/User")]
    [EnableCors("CorsApi")]
    public class UserControllerr : BaseController
    {
        private readonly IUserService _UserService;

        public UserControllerr(IUserService UserService , IAccountServices accountServices)
        {
            _UserService = UserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLists()
        {
            var searchParam = new SearchParam
            {
                ItemsPerPage = -1,
                SortBy = "created_date",
                SortDesc = false,
            };
            return Ok(await _UserService.GetPagingAsync(searchParam));
        }


        private async Task<PagingResult<UsersDTO>> GetListsData(SearchParam searchParam)
        {
            return await _UserService.GetPagingAsync(searchParam);
        }
        
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return JSonResult(await _UserService.GetByIdAsync(id));
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsersDTOCreate model)
        {
            return new JsonResult(await _UserService.CreateAsync(model));
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UsersDTO model)
        {
            return Ok(await _UserService.UpdateAsync(model));
        }
        
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        { 
            return Ok(await _UserService.DeleteAsync(id));
        }

        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _UserService.GetAllAsync());
        }

    }
}
