using DeviceService.Application.DTOS.City;
using DeviceService.Application.DTOS.Country;
using DeviceService.Application.DTOS.State;
using DeviceService.Application.DTOS.SupplierAccount;
using DeviceService.Application.DTOS.SupplierAddress;
using DeviceService.Application.DTOS.SupplierContact;

namespace DeviceService.Application.DTOS.Supplier
{
    public class SupplierDTODetail : SupplierDTO
    {
        public List<SupplierContactDTO> Contacts { get; set; }
        public List<SupplierAddressDTODetail> Addresses { get; set; }
        public List<SupplierAccountDTO> Accounts { get; set; }
        //public List<CountryDTO> Countries { get; set; }
        //public List<StateDTO> States { get; set; }
        //public List<CityDTO> Cities { get; set; }

    }
}
