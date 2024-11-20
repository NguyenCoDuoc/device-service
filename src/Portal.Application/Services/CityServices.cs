using AutoMapper;
using Microsoft.Extensions.Configuration;
using DeviceService.Application.ContextAccessors;
using DeviceService.Application.DTOS.City;
using DeviceService.Application.Interfaces;
using DeviceService.Domain.Interfaces;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Application.Services
{
	public class CityServices : ICityServices
    {
		#region Properties
		private readonly ICityRepository _cityRepository;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IConfiguration _configuration;
		private readonly IUserPrincipalService _CurrentUser;
		#endregion
		#region Ctor
		public CityServices(ICityRepository cityRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
			IUserPrincipalService userIdentity)
		{
			_cityRepository = cityRepository;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
			_CurrentUser = userIdentity;
		}
		#endregion
		#region Get
		public async Task<PagingResult<CityDTO>> GetPagingAsync(SearchParam pagingParams)
		{
			var result = new PagingResult<CityDTO>()
			{ PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
			var where = $"(name ILike @code)";
			var param = new Dictionary<string, object>();
			param.Add("code", $"%{pagingParams.Term}%");
			var data = await _cityRepository.GetPageAsync<CityDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
				order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
			result.Data = data.Data.ToList();
			result.TotalRows = data.TotalRow;
			return result;
		}
        public async Task<PagingResult<CityDTO>> GetPagingAsync(long CountryId,long StateId, SearchParam pagingParams)
        {
            var result = new PagingResult<CityDTO>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
            var where = $"(name ILike @code) AND country_id=@country_id AND state_id=@state_id";
            var param = new Dictionary<string, object>();
            param.Add("code", $"%{pagingParams.Term}%");
            param.Add("country_id", CountryId);
            param.Add("state_id", StateId);
            var data = await _cityRepository.GetPageAsync<CityDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
            result.Data = data.Data.ToList();
            result.TotalRows = data.TotalRow;
            return result;
        }
        #endregion


    }
}
