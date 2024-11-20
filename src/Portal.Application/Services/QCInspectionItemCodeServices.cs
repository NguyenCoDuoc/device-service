using AutoMapper;
using Microsoft.Extensions.Configuration;
using Portal.Application.ContextAccessors;
using Portal.Application.DTOS.QCInspectionItemCode;
using Portal.Application.Interfaces;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;

namespace Portal.Application.Services
{
    public class QCInspectionItemCodeServices : IQCInspectionItemCodeServices
    {
        #region Properties
        private readonly IQCInspectionItemCodeRepository _qcInspectionItemCodeRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserPrincipalService _CurrentUser;
        #endregion
        #region Ctor
        public QCInspectionItemCodeServices(IQCInspectionItemCodeRepository qcInspectionItemCodeRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
         IUserPrincipalService userIdentity)
        {
            _qcInspectionItemCodeRepository = qcInspectionItemCodeRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _CurrentUser = userIdentity;
        }
        #endregion

        #region Get
        public async Task<PagingResult<InspectionDashboardDto>> GetPagingAsync(SearchParam pagingParams)
        {
            var result = new PagingResult<InspectionDashboardDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
            var where = $"(ItemCode Like @code OR InspectionGroupCode Like @code OR InspectionItemCode like @code OR InspectionItemName like @code OR InspectionItemNameEN like @code OR InspectionItemNameCN like @code) AND Active=1";
            var param = new Dictionary<string, object>();
            param.Add("code", $"%{pagingParams.Term}%");
            var data = await _qcInspectionItemCodeRepository.GetPageAsync<InspectionDashboardDto>(pagingParams.Page, pagingParams.ItemsPerPage,order: pagingParams.SortBy,sortDesc: pagingParams.SortDesc, param: param, where: where);
            result.Data = data.Data.ToList();
            result.TotalRows = data.TotalRow;
            return result;
        }
        public async Task<ServiceResult> GetByIdAsync(long id)
        {
            var entity = await _qcInspectionItemCodeRepository.GetAsync(id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            return new ServiceResultSuccess(_mapper.Map<InspectionDashboardDto>(entity));
        }
        #endregion
       
    }
}
