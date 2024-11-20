using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using DeviceService.Application.ContextAccessors;
using DeviceService.Application.DTOS.Account;
using DeviceService.Application.DTOS.Supplier;
using DeviceService.Application.DTOS.SupplierAccount;
using DeviceService.Application.DTOS.SupplierAddress;
using DeviceService.Application.DTOS.SupplierContact;
using DeviceService.Application.DTOS.Users;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Services;
using DeviceService.Application.Validators;

namespace DeviceService.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeviceApplication(this IServiceCollection services)
        {
            services.AddScoped<ISupplierServices, SupplierServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<ICountryServices, CountryServices>();
            services.AddScoped<IStateServices, StateServices>();
            services.AddScoped<ICityServices, CityServices>();
            services.AddTransient<IValidator<LoginDTO>, LoginValidator>();
            services.AddTransient<IValidator<LogoutDTO>, LogoutValidator>();
            services.AddTransient<IValidator<UsersDTOCreate>, UsersCreateValidator>();
            services.AddTransient<IValidator<UsersDTOUpdate>, UsersUpdateValidator>();
            services.AddTransient<IValidator<UsersRequestDTO>, UsersRequestValidator>();
            services.AddTransient<IValidator<ChangePasswordDTO>, ChangePasswordValidator>();
            services.AddScoped<IUserPrincipalService, UserPrincipalService>();
			services.AddTransient<IValidator<SupplierDTOCreate>, SupplierCreateValidator>();
			services.AddTransient<IValidator<SupplierDTOUpdate>, SupplierUpdateValidator>();
			services.AddTransient<IValidator<SupplierAccountDTOCreate>, SupplierAccountCreateValidator>();
			services.AddTransient<IValidator<SupplierAccountDTOUpdate>, SupplierAccountUpdateValidator>();
			services.AddTransient<IValidator<SupplierContactDTOCreate>, SupplierContactCreateValidator>();
			services.AddTransient<IValidator<SupplierContactDTOUpdate>, SupplierContactUpdateValidator>();
			services.AddTransient<IValidator<SupplierAddressDTOCreate>, SupplierAddressCreateValidator>();
			services.AddTransient<IValidator<SupplierAddressDTOUpdate>, SupplierAddressUpdateValidator>();


            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddValidatorsFromAssemblyContaining<DepartmentValidator>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddValidatorsFromAssemblyContaining<LocationValidator>();
            services.AddScoped<IDeviceTypeService, DeviceTypeService>();
            services.AddValidatorsFromAssemblyContaining<DeviceTypeValidator>();
            services.AddScoped<IDeviceUnitService, DeviceUnitService>();
            services.AddValidatorsFromAssemblyContaining<DeviceUnitValidator>();
            services.AddScoped<IDeviceService, Services.DeviceService>();
            services.AddValidatorsFromAssemblyContaining<DeviceValidator>();
            services.AddScoped<IJobtitleService, JobtitleService>();
            services.AddScoped<ISerialLocationService, SerialLocationService>();
            services.AddScoped<ISerialService, SerialService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IAttributeValueService, AttributeValueService>();
            services.AddValidatorsFromAssemblyContaining<JobtitleValidator>();
            services.AddValidatorsFromAssemblyContaining<SerialLocationValidator>();
            services.AddValidatorsFromAssemblyContaining<SerialValidator>();
            services.AddValidatorsFromAssemblyContaining<AttributeValidator>();
            services.AddValidatorsFromAssemblyContaining<AttributeValueValidator>();

            return services;
        }
    }
}
