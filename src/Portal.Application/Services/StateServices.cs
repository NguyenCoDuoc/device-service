using AutoMapper;
using Microsoft.Extensions.Configuration;
using DeviceService.Application.ContextAccessors;
using DeviceService.Application.DTOS.State;
using DeviceService.Application.Interfaces;
using DeviceService.Domain.Interfaces;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Application.Services
{
	public class StateServices : IStateServices
    {
		#region Properties
		private readonly IStateRepository _stateRepository;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IConfiguration _configuration;
		private readonly IUserPrincipalService _CurrentUser;
		#endregion
		#region Ctor
		public StateServices(IStateRepository stateRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
			IUserPrincipalService userIdentity)
		{
			_stateRepository = stateRepository;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
			_CurrentUser = userIdentity;
		}
		#endregion
		#region Get
		public async Task<PagingResult<StateDTO>> GetPagingAsync(SearchParam pagingParams)
		{
			var result = new PagingResult<StateDTO>()
			{ PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
			var where = $"(name ILike @code OR code ILike @code)";
			var param = new Dictionary<string, object>();
			param.Add("code", $"%{pagingParams.Term}%");
			var data = await _stateRepository.GetPageAsync<StateDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
				order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
			result.Data = data.Data.ToList();
			result.TotalRows = data.TotalRow;
			return result;
		}
        public async Task<PagingResult<StateDTO>> GetPagingAsync(long CountryId, SearchParam pagingParams)
        {
            var result = new PagingResult<StateDTO>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
            var where = $"(name ILike @code OR code ILike @code) AND country_id=@country_id";
            var param = new Dictionary<string, object>();
            param.Add("code", $"%{pagingParams.Term}%");
            param.Add("country_id", CountryId);
            var data = await _stateRepository.GetPageAsync<StateDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
            result.Data = data.Data.ToList();
            result.TotalRows = data.TotalRow;
            return result;
        }
        #endregion


    }
}
