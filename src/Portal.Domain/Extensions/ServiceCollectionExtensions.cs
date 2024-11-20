using Microsoft.Extensions.DependencyInjection;
using DeviceService.Domain.Interfaces;
using DeviceService.Domain.Repositories;

namespace DeviceService.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeviceDomain(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ISupplierAccountRepository, SupplierAccountRepository>();
            services.AddScoped<ISupplierAddressRepository, SupplierAddressRepository>();
            services.AddScoped<ISupplierContactRepository, SupplierContactRepository>();
            services.AddScoped<IUsersSessionRepository, UsersSessionRepository>();
            services.AddScoped<ISHEmailTempRepository, SHEmailTempRepository>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
            services.AddScoped<IDeviceUnitRepository, DeviceUnitRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<ISerialRepository, SerialRepository>();
            services.AddScoped<ISerialLocationRepository, SerialLocationRepository>();
            services.AddScoped<IJobtitleRepository, JobtitleRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            return services;
        }
    }
}
