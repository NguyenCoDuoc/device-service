using AutoMapper;
using Microsoft.Extensions.Configuration;
using Portal.Application.ContextAccessors;
using Portal.Application.DTOS.CicmpyConsolidated;
using Portal.Application.DTOS.QCAQLDataSheet;
using Portal.Application.DTOS.QCInspectionGroup;
using Portal.Application.Interfaces;
using Portal.Domain.Interfaces;
using Portal.Domain.Repositories;
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
	public class QCAQLDataSheetServices : IQCAQLDataSheetServices
    {
		#region Properties
		private readonly IQCAQLDataSheetRepository _qCAQLDataSheetRepository;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IConfiguration _configuration;
		private readonly IUserPrincipalService _CurrentUser;
		#endregion
		#region Ctor
		public QCAQLDataSheetServices(IQCAQLDataSheetRepository qCAQLDataSheetRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
		 IUserPrincipalService userIdentity)
		{
            _qCAQLDataSheetRepository = qCAQLDataSheetRepository;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
			_CurrentUser = userIdentity;
		}
		#endregion

		#region Get
		public async Task<PagingResult<QCAQLDataSheetDTO>> GetPagingAsync(SearchParam pagingParams)
		{
			var result = new PagingResult<QCAQLDataSheetDTO>()
			{ PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
			var param = new Dictionary<string, object>();
			var data = await _qCAQLDataSheetRepository.GetPageAsync<QCAQLDataSheetDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
				order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param);
			result.Data = data.Data.ToList();
			result.TotalRows = data.TotalRow;
			return result;
		}
		public async Task<ServiceResult> GetByIdAsync(string id)
		{
			var entity = await _qCAQLDataSheetRepository.GetAsync( id);
			if (!entity.IsNotEmpty())
				return new ServiceResultError("This information does not exist");
			return new ServiceResultSuccess(_mapper.Map<QCAQLDataSheetDTO>(entity));
		}
		#endregion
	}
}
