using Microsoft.AspNetCore.Http;
using Sun.Core.Share.Constants;
using Sun.Core.Share.Helpers.Util;
using System.Net;

namespace DeviceService.Common.Helpers.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleDomainException(context, ex);
            }
        }
        private Task HandleDomainException(HttpContext context, Exception exception)
        {
            var response = new
            {
                Code = 400,
                Type = CommonConst.Error,
                Message = exception.Message
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            return context.Response.WriteAsync(Utils.Serialize(response));
        }
    }
}