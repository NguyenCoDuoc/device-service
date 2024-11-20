using AutoMapper;
using Microsoft.Extensions.Configuration;
using DeviceService.Application.ContextAccessors;
using DeviceService.Application.DTOS.Country;
using DeviceService.Application.Interfaces;
using DeviceService.Domain.Interfaces;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Application.Services
{
	public class CountryServices : ICountryServices
    {
		#region Properties
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IConfiguration _configuration;
		private readonly IUserPrincipalService _CurrentUser;
		#endregion
		#region Ctor
		public CountryServices(ICountryRepository countryRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
			IUserPrincipalService userIdentity)
		{
			_countryRepository = countryRepository;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
			_CurrentUser = userIdentity;
		}
		#endregion
		#region Get
		public async Task<PagingResult<CountryDTO>> GetPagingAsync(SearchParam pagingParams)
		{
			var result = new PagingResult<CountryDTO>()
			{ PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
			var where = $"(name ILike @code OR code ILike @code)";
			var param = new Dictionary<string, object>();
			param.Add("code", $"%{pagingParams.Term}%");
			var data = await _countryRepository.GetPageAsync<CountryDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
				order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
			result.Data = data.Data.ToList();
			result.TotalRows = data.TotalRow;
			return result;
		}
		public async Task<ServiceResult> GetByIdAsync(long id)
		{
			var entity = await _countryRepository.GetAsync(id);
			if (!entity.IsNotEmpty())
				return new ServiceResultError("This information does not exist");
			var data = _mapper.Map<CountryDTO>(entity);
			return new ServiceResultSuccess(data);
		}
		#endregion


	}
}
