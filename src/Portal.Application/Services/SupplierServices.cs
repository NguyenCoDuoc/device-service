using AutoMapper;
using Microsoft.Extensions.Configuration;
using DeviceService.Application.ContextAccessors;
using DeviceService.Application.DTOS.City;
using DeviceService.Application.DTOS.Country;
using DeviceService.Application.DTOS.State;
using DeviceService.Application.DTOS.Supplier;
using DeviceService.Application.DTOS.SupplierAccount;
using DeviceService.Application.DTOS.SupplierAddress;
using DeviceService.Application.DTOS.SupplierContact;
using DeviceService.Application.Interfaces;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Helpers.Attributes;
using Sun.Core.DataAccess.Helpers.Queries;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using System.Net;
using static DeviceService.Application.Helpers.Enums.EnumCommon;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeviceService.Application.Services
{
    public class SupplierServices : ISupplierServices
    {
        #region Properties
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierContactRepository _supplierContactRepository;
        private readonly ISupplierAddressRepository _supplierAddressRepository;
        private readonly ISupplierAccountRepository _supplierAccountRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserPrincipalService _CurrentUser;
        #endregion
        #region Ctor
        public SupplierServices(ISupplierRepository supplierRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
            IUserPrincipalService userIdentity
            , ISupplierContactRepository supplierContactRepository
            , ISupplierAddressRepository supplierAddressRepository
            , ISupplierAccountRepository supplierAccountRepository
            , ICountryRepository countryRepository
            , IStateRepository stateRepository
            , ICityRepository cityRepository)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _CurrentUser = userIdentity;
            _supplierContactRepository = supplierContactRepository;
            _supplierAddressRepository = supplierAddressRepository;
            _supplierAccountRepository = supplierAccountRepository;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
        }
        #endregion
        #region Get
        public async Task<PagingResult<SupplierDTO>> GetPagingAsync(SupplierSearchParam pagingParams)
        {
            var result = new PagingResult<SupplierDTO>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
            var where = $"(name ILike @code OR code ILike @code OR email ILike @code OR phone ILike @code OR fax ILike @code)";
            var param = new Dictionary<string, object>();
            param.Add("code", $"%{pagingParams.Term}%");
            if (pagingParams.Status >= 0 || pagingParams.Status.IsNotEmpty())
            {
                where += " AND (status=@status)";
                param.Add("status", pagingParams.Status);
            }
            var data = await _supplierRepository.GetPageAsync<SupplierDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
            result.Data = data.Data.ToList();
            result.TotalRows = data.TotalRow;
            return result;
        }
        public async Task<ServiceResult> GetByIdAsync(long id)
        {
            var entity = await _supplierRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var data = _mapper.Map<SupplierDTODetail>(entity);

            //var countries = _countryRepository.GetAll("").OrderBy(n => n.NumericCode).ToList() ?? new List<Country>();
            //var states = _stateRepository.GetListByField("", "country_id", entity.CountryId ?? 0).OrderBy(n => n.Code).ToList() ?? new List<State>(); ;
            //var cities = await _cityRepository.GetByCountryState(entity.CountryId ?? 0, entity.StateId ?? 0);

            data.CountryName = entity.CountryId > 0 ? (_countryRepository.Get(entity.CountryId)?? new Country()).Name : "";
            data.StateName = entity.StateId > 0  ? (_stateRepository.Get(entity.StateId)?? new State()).Name : "";
            data.CityName = entity.CityId > 0 ?( _cityRepository.Get( entity.CityId)?? new City()).Name : "";


            var _Contacts = await _supplierContactRepository.GetListByFieldAsync("", "supplier_id", entity.ID);
            var _Accounts = await _supplierAccountRepository.GetListByFieldAsync("", "supplier_id", entity.ID);
            var Columns = SqlHelper.GetPropertyOrColumnsAccess(default(SupplierAddress), CrudFieldType.All, false);
            List<string> modifiedColumns = Columns.Select(item => $"addr.{item}").ToList();
            var selectColumns = modifiedColumns.Count != 0 ? string.Join(", ", modifiedColumns) : "addr.*";
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"supplier_id",entity.ID },
            };
            var _Addresss = _supplierAddressRepository.Query<SupplierAddressDTODetail>($@"select {selectColumns},co.name as CountryName, s.name as StateName, c.name as CityName from supplier_address addr
                                                                                    left join countries co on co.id = addr.country_id
                                                                                    left join states s on s.id = addr.state_id
                                                                                    left join cities c on c.id = addr.city_id
                                                                                    where supplier_id=@supplier_id AND (addr.is_deleted='False' OR addr.is_deleted IS NULL)", param);

            data.Accounts = _mapper.Map<IEnumerable<SupplierAccountDTO>>(_Accounts).ToList();
            data.Contacts = _mapper.Map<IEnumerable<SupplierContactDTO>>(_Contacts).ToList();
            data.Addresses = _Addresss.ToList();
            

            return new ServiceResultSuccess(data);
        }
        #endregion
        #region Create
        public async Task<ServiceResult> CreateAsync(SupplierDTOCreate model)
        {
            model.CreatedBy = _CurrentUser.UserId;
            if (_CurrentUser.Parent > 0 && !_CurrentUser.IsAdministrator)
            {
                return new ServiceResultError("You do not have permission to create a user");
            }

            if (await _supplierRepository.CodeExists(model.Code))
            {
                return new ServiceResultError("This code already exists");
            }
            var entity = _mapper.Map<Supplier>(model);
            var data = await _supplierRepository.InsertAsync(entity);
            return new ServiceResultSuccess("Record added successfully", _mapper.Map<SupplierDTOCreate>(data));
        }
        #endregion
        #region Update
        public async Task<ServiceResult> UpdateAsync(SupplierDTOUpdate model)
        {

            var entity = await _supplierRepository.GetAsync(model.ID);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");

            if (await _supplierRepository.CodeExists(model.Code, model.ID))
            {
                return new ServiceResultError("This code already exists");
            }
            var entityUpdate = Other.BindUpdate<Supplier>(entity, _mapper.Map<Supplier>(model), _CurrentUser.UserId);
            var data = await _supplierRepository.UpdateAsync(entityUpdate);
            return new ServiceResultSuccess("Record updated successfully", _mapper.Map<SupplierDTOUpdate>(data));
        }
        #endregion
        #region Update Status
        public async Task<ServiceResult> UpdateStatusAsync(long id, int Status)
        {

            var entity = await _supplierRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            entity.Status = Status;
            var data = await _supplierRepository.UpdateAsync(entity, Columns: new List<string> { "status" });
            return new ServiceResultSuccess("Record updated status successfully");
        }
        #endregion
        #region Delete
        public async Task<ServiceResult> DeleteAsync(long id)
        {
            var entity = await _supplierRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var rs = await _supplierRepository.DeleteAsync(id, _CurrentUser.UserId);
            return new ServiceResultSuccess("Record deleted successfully", rs);
        }
        #endregion


        #region Liên hệ
        #region Create
        public async Task<ServiceResult> GetByContactAsync(long SupplieId)
        {
            var entity = await _supplierRepository.GetAsync(SupplieId);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var _Contacts = await _supplierContactRepository.GetListByFieldAsync("", "supplier_id", entity.ID);
            return new ServiceResultSuccess(_mapper.Map<IEnumerable<SupplierContactDTO>>(_Contacts).ToList());
        }
        public async Task<ServiceResult> CreateContactAsync(long id, SupplierContactDTOCreate model)
        {
            model.CreatedBy = _CurrentUser.UserId;
            model.SupplierId = id;
            if (_CurrentUser.Parent > 0 && !_CurrentUser.IsAdministrator)
            {
                return new ServiceResultError("You do not have permission to create a user");
            }
            var entity = _mapper.Map<SupplierContact>(model);
            var data = await _supplierContactRepository.InsertAsync(entity);
            return new ServiceResultSuccess("Record added successfully", _mapper.Map<SupplierContactDTOCreate>(data));
        }
        #endregion
        #region Update
        public async Task<ServiceResult> UpdateContactAsync(long id, SupplierContactDTOUpdate model)
        {
            model.SupplierId = id;
            var entity = await _supplierContactRepository.GetAsync(model.ID);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var entityUpdate = Other.BindUpdate<SupplierContact>(entity, _mapper.Map<SupplierContact>(model), _CurrentUser.UserId);
            var data = await _supplierContactRepository.UpdateAsync(entityUpdate);
            return new ServiceResultSuccess("Record updated successfully", _mapper.Map<SupplierContactDTOUpdate>(data));
        }


        #endregion
        #region Delete
        public async Task<ServiceResult> DeleteContactAsync(long id)
        {
            var entity = await _supplierContactRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var rs = await _supplierContactRepository.DeleteAsync(entity.ID, _CurrentUser.UserId);
            return new ServiceResultSuccess("Record deleted successfully", rs);
        }
        #endregion
        #endregion
        #region Địa chỉ
        #region Create
        public async Task<ServiceResult> GetByAddressAsync(long SupplieId)
        {
            var entity = await _supplierRepository.GetAsync(SupplieId);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var Columns = SqlHelper.GetPropertyOrColumnsAccess(default(SupplierAddress), CrudFieldType.All, false);
            List<string> modifiedColumns = Columns.Select(item => $"addr.{item}").ToList();
            var selectColumns = modifiedColumns.Count != 0 ? string.Join(", ", modifiedColumns) : "addr.*";
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"supplier_id",entity.ID },
            };
           
            var _Addresss = _supplierAddressRepository.Query<SupplierAddressDTODetail>($@"select {selectColumns},co.name as CountryName, s.name as StateName, c.name as CityName from supplier_address addr
                                                                                    left join countries co on co.id = addr.country_id
                                                                                    left join states s on s.id = addr.state_id
                                                                                    left join cities c on c.id = addr.city_id
                                                                                    where supplier_id=@supplier_id AND (addr.is_deleted='False' OR addr.is_deleted IS NULL)", param);

            return new ServiceResultSuccess(_Addresss);
        }
        public async Task<ServiceResult> CreateAddressAsync(long id, SupplierAddressDTOCreate model)
        {
            model.CreatedBy = _CurrentUser.UserId;
            model.SupplierId = id;
            if (_CurrentUser.Parent > 0 && !_CurrentUser.IsAdministrator)
            {
                return new ServiceResultError("You do not have permission to create a user");
            }
            var entity = _mapper.Map<SupplierAddress>(model);
            var data = await _supplierAddressRepository.InsertAsync(entity);
            return new ServiceResultSuccess("Record added successfully", _mapper.Map<SupplierAddressDTOCreate>(data));
        }
        #endregion
        #region Update
        public async Task<ServiceResult> UpdateAddressAsync(long id, SupplierAddressDTOUpdate model)
        {
            model.SupplierId = id;
            var entity = await _supplierAddressRepository.GetAsync(model.ID);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var entityUpdate = Other.BindUpdate<SupplierAddress>(entity, _mapper.Map<SupplierAddress>(model), _CurrentUser.UserId);
            var data = await _supplierAddressRepository.UpdateAsync(entityUpdate);
            return new ServiceResultSuccess("Record updated successfully", _mapper.Map<SupplierAddressDTOUpdate>(data));
        }


        #endregion
        #region Delete
        public async Task<ServiceResult> DeleteAddressAsync(long id)
        {
            var entity = await _supplierAddressRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");

            var rs = await _supplierAddressRepository.DeleteAsync(id, _CurrentUser.UserId);
            return new ServiceResultSuccess("Record deleted successfully", rs);
        }
        #endregion
        #endregion
        #region Ngân hàng
        #region Create
        public async Task<ServiceResult> GetByAccountAsync(long SupplieId)
        {
            var entity = await _supplierRepository.GetAsync(SupplieId);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var _Accounts = await _supplierAccountRepository.GetListByFieldAsync("", "supplier_id", entity.ID);
            return new ServiceResultSuccess(_mapper.Map<IEnumerable<SupplierAccountDTO>>(_Accounts).ToList());
        }
        public async Task<ServiceResult> CreateAccountAsync(long id, SupplierAccountDTOCreate model)
        {
            model.CreatedBy = _CurrentUser.UserId;
            model.SupplierId = id;
            if (_CurrentUser.Parent > 0 && !_CurrentUser.IsAdministrator)
            {
                return new ServiceResultError("You do not have permission to create a user");
            }
            var entity = _mapper.Map<SupplierAccount>(model);
            var data = await _supplierAccountRepository.InsertAsync(entity);
            return new ServiceResultSuccess("Record added successfully", _mapper.Map<SupplierAccountDTOCreate>(data));
        }
        #endregion
        #region Update
        public async Task<ServiceResult> UpdateAccountAsync(long id, SupplierAccountDTOUpdate model)
        {
            model.SupplierId = id;
            var entity = await _supplierAccountRepository.GetAsync(model.ID);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");

            var entityUpdate = Other.BindUpdate<SupplierAccount>(entity, _mapper.Map<SupplierAccount>(model), _CurrentUser.UserId);
            var data = await _supplierAccountRepository.UpdateAsync(entityUpdate);
            return new ServiceResultSuccess("Record updated successfully", _mapper.Map<SupplierAccountDTOUpdate>(data));
        }


        #endregion
        #region Delete
        public async Task<ServiceResult> DeleteAccountAsync(long id)
        {
            var entity = await _supplierAccountRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var rs = await _supplierAccountRepository.DeleteAsync(id, _CurrentUser.UserId);
            return new ServiceResultSuccess("Record deleted successfully", rs);
        }
        #endregion
        #endregion

    }
}
