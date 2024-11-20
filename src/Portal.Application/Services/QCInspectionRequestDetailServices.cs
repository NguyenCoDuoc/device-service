using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.GeometriesGraph;
using Portal.Application.ContextAccessors;
using Portal.Application.DTOS.QCInspectionItemCode;
using Portal.Application.DTOS.QCInspectionRequest;
using Portal.Application.DTOS.QCInspectionRequestDetail;
using Portal.Application.DTOS.QCInspectionRequestDetailResult;
using Portal.Application.DTOS.Users;
using Portal.Application.Helpers.Enums;
using Portal.Application.Interfaces;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Domain.Repositories;
using Sun.Core.Logging.Interfaces;
using Sun.Core.RabbitMQ.Services;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using System.Data;
using static Dapper.SqlMapper;
using static Portal.Application.Helpers.Enums.EnumCommon;

namespace Portal.Application.Services
{
    public class QCInspectionRequestDetailServices : IQCInspectionRequestDetailServices
    {
        #region Properties
        private readonly IQCInspectionRequestMasterRepository _masterRepository;
        private readonly IQCInspectionRequestDetailRepository _detailRepository;
        private readonly IQCInspectionRequestDetailTemplateRepository _detailTemplateRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserPrincipalService _CurrentUser;
        //private readonly IRabbitMQService _rabbitMqService;
       // private readonly ISHEmailTempServices _shEmailTempServices;
        #endregion
        #region Ctor
        public QCInspectionRequestDetailServices(IQCInspectionRequestMasterRepository masterRepository,
            IQCInspectionRequestDetailRepository detailRepository,
            IQCInspectionRequestDetailTemplateRepository detailTemplateRepository
            , IMapper mapper, ILoggerManager logger, IConfiguration configuration,
         IUserPrincipalService userIdentity, IUsersRepository usersRepository,
         IRabbitMQService rabbitMqService, ISHEmailTempServices shEmailTempServices)
        {
            _masterRepository = masterRepository;
            _detailTemplateRepository = detailTemplateRepository;
            _detailRepository = detailRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _CurrentUser = userIdentity;
            _usersRepository = usersRepository;
            _rabbitMqService = rabbitMqService;
            _shEmailTempServices = shEmailTempServices;
        }
        #endregion
        #region Get
        public async Task<PagingResult<QCInspectionRequestDetailDTO>> GetPagingAsync(QCInspectionRequestSearchParam pagingParams)
        {
            var result = new PagingResult<QCInspectionRequestDetailDTO>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            DynamicParameters param = new DynamicParameters();
            param.Add("@Term", pagingParams.Term);
            param.Add("@Status", pagingParams.Status);
            param.Add("@FromDate", pagingParams.FromDate);
            param.Add("@EndDate", pagingParams.ToDate);
            param.Add("@cmp_wwn", _CurrentUser.cmp_wwn);
            param.Add("@pageNumber", pagingParams.Page);
            param.Add("@pageSize", pagingParams.ItemsPerPage);
            param.Add("@order", pagingParams.SortBy);
            param.Add("@sort", pagingParams.SortDesc ? "Desc" : "asc");
            param.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var lst = await _detailRepository.ProcedureAsync<QCInspectionRequestDetailDTO>("PROC_QCInspectionRequestDetail_GetData", param);
            result.TotalRows = param.Get<int>("@totalRecord");
            result.Data = lst.ToList();
            return result;
        }
        public async Task<ServiceResult> GetByItem(string ItemCode, double LotSize)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@LotSize", LotSize);
            param.Add("@ItemCode", ItemCode);
            var lst = await _detailRepository.ProcedureAsync<QCInspectionRequestDetailResultDTODetail>("PROC_QCInspectionItemCode_GetByItemCode", param);
            return new ServiceResultSuccess(lst);
        }
        #endregion
        #region Update Detail and  Create DetailResult
        public async Task<ServiceResult> CreateAsync(QCInspectionRequestDetailDTOUpdate detail, List<QCInspectionRequestDetailResultDTOCreate> detailResults)
        {
            var detailmater = await _detailRepository.GetAsync(detail.Id);
            if (!detailmater.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            if (detailmater.IsNotEmpty() && detailmater.Active == false)
                return new ServiceResultError("This information detail does not exist");
            if (detailResults.Count <= 0)
            {
                return new ServiceResultError("The list must not be empty, or IdDetail must not be empty");
            }
            var entityUpdate = Other.BindUpdate<QCInspectionRequestDetail>(detailmater, detail, _CurrentUser.UserId);
            var data = await _detailRepository.UpdateAsync(entityUpdate);

            var Templates = await _detailTemplateRepository.GetListByFieldAsync("DetailId", detailmater.Id);
            if (Templates.Any())
                await _detailTemplateRepository.DeletesAsync(Templates.ToList());
            var DetailTemplateInspections = _mapper.Map<List<QCInspectionRequestDetailResult>>(detailResults);
            foreach (var item in DetailTemplateInspections)
            {
                item.Id = Guid.NewGuid().ToString();
                item.DetailId = detailmater.Id;
                item.MasterId = detailmater.MasterId;
                item.CreateBy = _CurrentUser.UserName;
            }
            await _detailTemplateRepository.InsertsAsync(DetailTemplateInspections, false);
            if (data.Status == "WAIT")
            {
                await UpdateStatusAsync(detailmater.Id, "PROCESSING");
            }
            return new ServiceResultSuccess("Record updated successfully");
        }
        #endregion
        #region Update DetailResult
        public async Task<ServiceResult> UpdateDetailResultAsync(QCInspectionRequestDetailResultDTOUpdate model)
        {
            var detailmater = await _detailRepository.GetAsync(model.DetailId);
            if (!detailmater.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            if (detailmater.IsNotEmpty() && detailmater.Active == false)
                return new ServiceResultError("This information detail does not exist");
            var detailResult = await _detailTemplateRepository.GetAsync(model.Id);
            if (!detailResult.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var entityUpdate = Other.BindUpdate<QCInspectionRequestDetailResult>(detailResult, model, _CurrentUser.UserId);
            entityUpdate.ModifyBy = _CurrentUser.UserName;
            var data = await _detailTemplateRepository.UpdateAsync(entityUpdate);
            return new ServiceResultSuccess("Record updated successfully");
        }
        #endregion
        #region Update Status
        public async Task<ServiceResult> UpdateStatusAsync(string Id, string Status)
        {
            var detail = await _detailRepository.GetAsync(Id);
            if (!detail.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            if (detail.IsNotEmpty() && detail.Active == false)
                return new ServiceResultError("This information detail does not exist");
            if (detail.Status == "DONE" || detail.Status == "COMPLETED" || detail.Status == "SHREJECT")
                return new ServiceResultError("Unable to update status");
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            param.Add("@Status", Status);
            param.Add("@ModifyBy", _CurrentUser.UserName);
            var Email = "thuypt2@sunhouse.com.vn";//_masterRepository.QueryFirstOrDefault<string>($"SELECT  (CASE WHEN Email <>'' OR Email IS NOT NULL THEN Email ELSE Email2 END) Email FROM dbo.AppUsers WHERE Active=1 AND UserName ='{entity.CreateBy}'");
            await _detailRepository.ProcedureExecuteAsync("PROC_QCInspectionRequestDetail_UpdateStatus", param);
            if(Status== "DONE")
            {
                var entity = await _masterRepository.GetAsync(detail.MasterId);
                if (Email.IsNotEmpty())
                {
                    var replaceSubject = new Dictionary<string, string>
                {
                    {"{0}",_CurrentUser.UserName },
                    {"{1}",entity.RequestNumber },
                };
                    var replacements = new Dictionary<string, string>
                {
                    {"{0}",entity.RequestNumber },
                    {"{1}",detail.ordernr },
                    {"{2}",detail.ItemCode },
                    {"{2}",_CurrentUser.FullName },
                };

                    _rabbitMqService.ConfigureRabbitMQ("sendmail_exchange", "topic", "sendmail_queue", "sendmail_routing_key");
                    var message = await _shEmailTempServices.GetTempateAsync(Email, SendMailType.SendMaillResult);
                    message.Subject = Utils.ReplaceValues(message.Subject, replaceSubject);
                    message.Body = Utils.ReplaceValues(message.Body, replacements);
                    _rabbitMqService.PublishMessage("sendmail_exchange", "sendmail_routing_key", message);
                }
                    
            }    
           
            return new ServiceResultSuccess("Record updated successfully");
        }
        #endregion
        #region Clone Detail
        public async Task<ServiceResult> CloneAsync(string Id)
        {
            var detail = await _detailRepository.GetAsync(Id);
            if (!detail.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            if (detail.Status != "SHREJECT")
                return new ServiceResultError("Unable to create new");
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            param.Add("@CreateBy", _CurrentUser.UserName);
            var detailnew= await _detailRepository.ProcedureFirstAsync<QCInspectionRequestDetail>("PROC_QCInspectionRequestDetail_Clone", param);
            var data = _mapper.Map<QCInspectionRequestDetailDTO>(detailnew);
            return new ServiceResultSuccess("Record Clone successfully", data);
        }
        #endregion

    }
}
