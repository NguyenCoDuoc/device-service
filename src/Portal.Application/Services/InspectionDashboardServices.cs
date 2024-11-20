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
    public class InspectionDashboardServices : IInspectionDashboardServices
    {
        #region Properties
        private readonly IInspectionDashboardRepository _inspectionDashboardRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Ctor
        public InspectionDashboardServices(IInspectionDashboardRepository inspectionDashboardRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration,
         IUserPrincipalService userIdentity)
        {
            _inspectionDashboardRepository = inspectionDashboardRepository;
            _mapper = mapper;
        }
        #endregion

        #region Get
        public async Task<ServiceResult> GetStaticStatus(string cmp, int month, int year, bool isViewRequest)
        {
            var entity = await _inspectionDashboardRepository.GetStaticStatus(cmp, month, year, isViewRequest);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            return new ServiceResultSuccess(_mapper.Map<InspectionDashboardDto>(entity));
        }
        #endregion
       
    }
}
