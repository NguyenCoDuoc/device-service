using AutoMapper;
using Microsoft.Extensions.Configuration;
using Portal.Application.ContextAccessors;
using Portal.Application.DTOS.CicmpyConsolidated;
using Portal.Application.DTOS.QCInspectionGroup;
using Portal.Application.Interfaces;
using Portal.Domain.Interfaces;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
	public class CicmpyConsolidatedServices : ICicmpyConsolidatedServices
	{
		#region Properties
		private readonly ICicmpyConsolidatedRepository _cicmpyConsolidatedRepository;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IConfiguration _configuration;
		private readonly IUserPrincipalService _CurrentUser;
		#endregion
		#region Ctor
		public CicmpyConsolidatedServices(ICicmpyConsolidatedRepository cicmpyConsolidatedRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
		 IUserPrincipalService userIdentity)
		{
			_cicmpyConsolidatedRepository = cicmpyConsolidatedRepository;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
			_CurrentUser = userIdentity;
		}
		#endregion

		#region Get
		public async Task<PagingResult<CicmpyConsolidatedDTO>> GetPagingAsync(SearchParam pagingParams)
		{
			var result = new PagingResult<CicmpyConsolidatedDTO>()
			{ PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
			var where = $"({nameof(CicmpyConsolidatedDTO.cmp_code)} Like @code OR {nameof(CicmpyConsolidatedDTO.cmp_name)} like @code ) AND Active=1 " +
				$" AND crdnr IS NOT NULL AND cmp_type = 'S' AND Division = 101 AND ISNULL(DbSource,'101') = '101'";
			var param = new Dictionary<string, object>();
			param.Add("code", $"%{pagingParams.Term}%");
			var data = await _cicmpyConsolidatedRepository.GetPageAsync<CicmpyConsolidatedDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
				order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
			result.Data = data.Data.ToList();
			result.TotalRows = data.TotalRow;
			return result;
		}
		public async Task<ServiceResult> GetByIdAsync(string id)
		{
			var entity = await _cicmpyConsolidatedRepository.GetFieldAsync(nameof(CicmpyConsolidatedDTO.cmp_wwn), id);
			if (!entity.IsNotEmpty())
				return new ServiceResultError("This information does not exist");
			return new ServiceResultSuccess(_mapper.Map<CicmpyConsolidatedDTO>(entity));
		}
		#endregion
	}
}
