
using DeviceService.Application.Mappings;
using DeviceService.Common.Extensions;
using DeviceService.Domain.Extensions;
using DeviceService.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddServiceCollection(configuration);
builder.Services.AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddDeviceDomain();
builder.Services.AddDeviceApplication();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();
app.ConfigureWebApplication(configuration);

app.Run();




