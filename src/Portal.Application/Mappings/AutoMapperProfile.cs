using AutoMapper;
using DeviceService.Application.DTOS;
using DeviceService.Application.DTOS.City;
using DeviceService.Application.DTOS.Country;
using DeviceService.Application.DTOS.State;
using DeviceService.Application.DTOS.Supplier;
using DeviceService.Application.DTOS.SupplierAccount;
using DeviceService.Application.DTOS.SupplierAddress;
using DeviceService.Application.DTOS.SupplierContact;
using DeviceService.Application.DTOS.Users;
using DeviceService.Application.DTOS.UsersSession;
using DeviceService.Domain.Entities;
using Attribute = DeviceService.Domain.Entities.Attribute;

namespace DeviceService.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Users, UsersDTO>().ReverseMap();
        CreateMap<Users, UsersDTOCreate>().ReverseMap();
        CreateMap<Users, UsersDTOUpdate>().ReverseMap();
       
        CreateMap<Users, UpdateProfileDTO>().ReverseMap();
        CreateMap<UsersSession, UsersSessionDTO>().ReverseMap();
        CreateMap<UsersSession, UsersSessionDTOCreate>().ReverseMap();

		CreateMap<Country, CountryDTO>().ReverseMap();
		CreateMap<City, CityDTO>().ReverseMap();
		CreateMap<State, StateDTO>().ReverseMap();
		CreateMap<Supplier, SupplierDTOCreate>().ReverseMap();
		CreateMap<Supplier, SupplierDTOUpdate>().ReverseMap();
		CreateMap<Supplier, SupplierDTODetail>().ReverseMap();
		CreateMap<Supplier, SupplierDTODetail>().ReverseMap();
		CreateMap<SupplierAccount, SupplierAccountDTO>().ReverseMap();
		CreateMap<SupplierAccount, SupplierAccountDTOCreate>().ReverseMap();
		CreateMap<SupplierAccount, SupplierAccountDTOUpdate>().ReverseMap();
		CreateMap<SupplierAddress, SupplierAddressDTO>().ReverseMap();
		CreateMap<SupplierAddress, SupplierAddressDTOCreate>().ReverseMap();
		CreateMap<SupplierAddress, SupplierAddressDTOUpdate>().ReverseMap();
		CreateMap<SupplierAddress, SupplierAddressDTODetail>().ReverseMap();
		CreateMap<SupplierContact, SupplierContactDTO>().ReverseMap();
		CreateMap<SupplierContact, SupplierContactDTOCreate>().ReverseMap();
		CreateMap<SupplierContact, SupplierContactDTOUpdate>().ReverseMap();

        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<Department, DepartmentDtoCreate>().ReverseMap();
        CreateMap<Department, DepartmentDtoDetail>().ReverseMap();
        CreateMap<Location, LocationDto>().ReverseMap();
        CreateMap<Location, LocationDtoCreate>().ReverseMap();
        CreateMap<Location, LocationDtoDetail>().ReverseMap();
        CreateMap<DeviceType, DeviceTypeDto>().ReverseMap();
        CreateMap<DeviceType, DeviceTypeDtoCreate>().ReverseMap();
        CreateMap<DeviceType, DeviceTypeDtoDetail>().ReverseMap();
        CreateMap<DeviceUnit, DeviceUnitDto>().ReverseMap();
        CreateMap<DeviceUnit, DeviceUnitDtoCreate>().ReverseMap();
        CreateMap<DeviceUnit, DeviceUnitDtoDetail>().ReverseMap();
        CreateMap<Device, DeviceDto>().ReverseMap();
        CreateMap<Device, DeviceDtoCreate>().ReverseMap();
        CreateMap<Device, DeviceDtoDetail>().ReverseMap();
        CreateMap<Attribute, AttributeDto>().ReverseMap();
        CreateMap<Attribute, AttributeDtoCreate>().ReverseMap();
        CreateMap<Attribute, AttributeDtoDetail>().ReverseMap();
        CreateMap<SerialLocation, SerialLocationDto>().ReverseMap();
        CreateMap<SerialLocation, SerialLocationDtoCreate>().ReverseMap();
        CreateMap<SerialLocation, SerialLocationDtoDetail>().ReverseMap();
        CreateMap<Serial, SerialDto>().ReverseMap();
        CreateMap<Serial, SerialDtoCreate>().ReverseMap();
        CreateMap<Serial, SerialDtoDetail>().ReverseMap();
        CreateMap<Jobtitle, JobtitleDto>().ReverseMap();
        CreateMap<Jobtitle, JobtitleDtoCreate>().ReverseMap();
        CreateMap<Jobtitle, JobtitleDtoDetail>().ReverseMap();
        CreateMap<AttributeValue, AttributeValueDto>().ReverseMap();
        CreateMap<AttributeValue, AttributeValueDtoCreate>().ReverseMap();
        CreateMap<AttributeValue, AttributeValueDtoDetail>().ReverseMap();
        CreateMap<DeviceTypeAttribute, DeviceTypeAttributeDto>().ReverseMap();
        CreateMap<DeviceTypeAttribute, DeviceTypeAttributeDtoCreate>().ReverseMap();

        // ...
    }
}