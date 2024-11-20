using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sun.Core.Share.Constants;
using Sun.Core.Share.Helpers.Results;
using System.Text.Json;

namespace DeviceService.Common.Controllers
{
    [EnableCors("CorsApi")]
    public class BaseController : ControllerBase, IActionFilter
    {

        /// <summary>
        /// Trả về JSon file theo kết quả Service trả về
        /// </summary>
        /// <param name="ServiceResult"></param>
        /// <returns></returns>
        protected IActionResult JSonResult(ServiceResult ServiceResult)
        {
            if (ServiceResult.Code == CommonConst.Success)
            {
                return JSSuccessResult(ServiceResult.Message, ServiceResult.Data);
            }
            else if (ServiceResult.Code == CommonConst.Error)
            {
                return JSErrorResult(ServiceResult.Message, ServiceResult.Data);
            }
            else if (ServiceResult.Code == CommonConst.Warning)
            {
                return JSWarningResult(ServiceResult.Message, ServiceResult.Data);
            }
            else if (ServiceResult.Code == CommonConst.Info)
            {
                return JSInfoResult(ServiceResult.Message, ServiceResult.Data);
            }
            else
            {
                return BadRequest(ServiceResult);
            }
        }

        protected IActionResult JSSuccessResult(string msg)
        {
            return new JsonResult(new
            {
                Code = 200,
                Type = CommonConst.Success,
                Message = msg
            });
        }

        protected IActionResult JSSuccessResult<T>(string msg, T val)
        {
            return new JsonResult(new
            {
                Code = 200,
                Type = CommonConst.Success,
                Message = msg,
                Data = val
            }, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
        protected IActionResult JSSuccessResult<T>(T val)
        {
            return new JsonResult(new
            {
                Code = 200,
                Type = CommonConst.Success,
                Data = val
            });
        }

        protected IActionResult JSErrorResult(string msg)
        {
            return new JsonResult(new
            {
                Code = 400,
                Type = CommonConst.Error,
                Message = msg
            });
        }

        protected IActionResult JSErrorResult<T>(string msg, T val)
        {
            return new JsonResult(new
            {
                Code = 400,
                Type = CommonConst.Error,
                Message = msg,
                Data = val
            });
        }


        protected IActionResult JSWarningResult(string msg)
        {
            return new JsonResult(new
            {
                Code = 400,
                Type = CommonConst.Warning,
                Message = msg
            });
        }

        protected IActionResult JSWarningResult<T>(string msg, T val)
        {
            return new JsonResult(new
            {
                Code = 400,
                Type = CommonConst.Warning,
                Message = msg,
                Data = val
            });
        }
        protected IActionResult JSInfoResult(string msg)
        {
            return new JsonResult(new
            {
                Code = 100,
                Type = CommonConst.Info,
                Message = msg
            });
        }

        protected IActionResult JSInfoResult<T>(string msg, T val)
        {
            return new JsonResult(new
            {
                Code = 100,
                Type = CommonConst.Info,
                Message = msg,
                Data = val
            });
        }
        [Microsoft.AspNetCore.Mvc.NonAction]
        public virtual void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
        [Microsoft.AspNetCore.Mvc.NonAction]
        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

    }
}
