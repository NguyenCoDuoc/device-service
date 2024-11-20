using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using DeviceService.Common.Helpers.Middleware;
using DeviceService.Common.Models;

namespace DeviceService.Common.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// cấu hình mặc định app cho ứng dụng
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication ConfigureWebApplication(this WebApplication app, IConfiguration configuration)
    {
        app.UseRouting();
        app.UseCors("CorsApi");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        // Đặt middleware Swagger trước khi gọi MapControllers
        // Đọc cấu hình Swagger từ appsettings.json
        var swaggerConfig = configuration.GetSection("Swagger").Get<SwaggerConfig>();
        if (swaggerConfig != null)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{swaggerConfig.Title} {swaggerConfig.Version}");
                c.RoutePrefix = string.Empty; // Để hiển thị Swagger UI tại root
            });
        }

        return app;
    }
}
