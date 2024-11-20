using AutoMapper;
using Microsoft.Extensions.Configuration;
using Portal.Application.ContextAccessors;
using Portal.Application.DTOS.QCInspectionGroup;
using Portal.Application.Interfaces;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Sun.Core.Logging.Interfaces;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;

namespace Portal.Application.Services
{
    public class QCInspectionGroupServices : IQCInspectionGroupServices
    {
        #region Properties
        private readonly IQCInspectionGroupRepository _qcInspectionGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserPrincipalService _CurrentUser;
        #endregion
        #region Ctor
        public QCInspectionGroupServices(IQCInspectionGroupRepository qcInspectionGroupRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
         IUserPrincipalService userIdentity)
        {
            _qcInspectionGroupRepository = qcInspectionGroupRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _CurrentUser = userIdentity;
        }
        #endregion

        #region Get
        public async Task<PagingResult<QCInspectionGroupDTO>> GetPagingAsync(SearchParam pagingParams)
        {
            var result = new PagingResult<QCInspectionGroupDTO>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
            var where = $"(InspectionGroupCode Like @code OR InspectionGroupName like @code OR InspectionGroupNameEN like @code OR InspectionGroupNameCN like @code) AND Active=1";
            var param = new Dictionary<string, object>();
            param.Add("code", $"%{pagingParams.Term}%");
            var data = await _qcInspectionGroupRepository.GetPageAsync<QCInspectionGroupDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
            result.Data = data.Data.ToList();
            result.TotalRows = data.TotalRow;
            return result;
        }
        public async Task<ServiceResult> GetByCodeAsync(string Code)
        {
            var entity = await _qcInspectionGroupRepository.GetFieldAsync("InspectionGroupCode", Code);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            return new ServiceResultSuccess(_mapper.Map<QCInspectionGroupDTO>(entity));
        }
        #endregion
        
    }
}
