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
using Sun.Core.Email.Models;
using Sun.Core.Logging.Interfaces;
using Sun.Core.RabbitMQ.Services;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Portal.Application.Helpers.Enums.EnumCommon;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portal.Application.Services
{
    public class QCInspectionRequestServices : IQCInspectionRequestServices
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
        private readonly IRabbitMQService _rabbitMqService;
        private readonly ISHEmailTempServices _shEmailTempServices;
        #endregion
        #region Ctor
        public QCInspectionRequestServices(IQCInspectionRequestMasterRepository masterRepository,
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
        public async Task<PagingResult<QCInspectionRequestMasterDTO>> GetPagingAsync(QCInspectionRequestSearchParam pagingParams)
        {
            var result = new PagingResult<QCInspectionRequestMasterDTO>()
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
            var lst = await _detailRepository.ProcedureAsync<QCInspectionRequestMasterDTO>("PROC_QCInspectionRequest_GetData", param);
            result.TotalRows = param.Get<int>("@totalRecord");
            result.Data = lst.ToList();
            return result;
        }
        public async Task<ServiceResult> GetByIdAsync(string Id)
        {
            var entity = await _masterRepository.GetAsync(Id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information does not exist");
            var details = await _detailRepository.GetListByFieldAsync("MasterId", Id);
            details = details.ToList() ?? new List<QCInspectionRequestDetail>();
            var data = _mapper.Map<QCInspectionRequestMasterDTODetail>(entity);
            data.Details = _mapper.Map<List<QCInspectionRequestDetailDTO>>(details.Where(n => n.Active));
            return new ServiceResultSuccess(data);
        }
        public async Task<ServiceResult> GetByIdAsync(string Id, string DetailId)
        {
            //var entity = await _masterRepository.GetAsync(Id);
            //if (!entity.IsNotEmpty())
            //	return new ServiceResultError("This information maters does not exist");
            var detail = await _detailRepository.GetAsync(DetailId);
            if (!detail.IsNotEmpty())
                return new ServiceResultError("This information detail does not exist");
            if (detail.IsNotEmpty() && detail.Active == false)
                return new ServiceResultError("This information detail does not exist");
            var entity = await _masterRepository.GetAsync(detail.MasterId);
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", DetailId);
            var detailTemplates = (await _detailTemplateRepository.ProcedureAsync<QCInspectionRequestDetailResultDTO>("PROC_QCInspectionRequestDetailResult_GetById", param)).ToList() ?? new List<QCInspectionRequestDetailResultDTO>();
            var data = _mapper.Map<QCInspectionRequestDetailDTO>(detail);
            data.RequestNumber = entity.RequestNumber;
            if (detailTemplates != null && detailTemplates.Count(n => n.Decision == "NG" || n.Decision == "OK") == 0)
                data.KQ = "";
            else if (detailTemplates != null && detailTemplates.Any(n => n.Decision == "NG"))
                data.KQ = "NG";
            else data.KQ = "OK";
            data.DetailResults = detailTemplates;
            return new ServiceResultSuccess(data);
        }

        #endregion
      
        #region Update status
        public async Task<ServiceResult> UpdateStatusAsync(QCInspectionRequestStatusDTO model)
        {

            _rabbitMqService.ConfigureRabbitMQ("sendmail_exchange", "topic", "sendmail_queue", "sendmail_routing_key");
            var entity = await _masterRepository.GetAsync(model.Id);
            if (!entity.IsNotEmpty())
                return new ServiceResultError("This information maters does not exist");
            var status = model.Status;
            if (entity.Status == "NEW" || entity.Status == "CONFIRMED" || entity.Status == "REJECT" || entity.Status == "DONE" || entity.Status == "COMPLETED" || entity.Status == "SHREJECT")
            {
                return new ServiceResultError("Unable to update status");
            }
            entity.Status = model.Status;
            entity.ApprovalBy = _CurrentUser.UserName;
            entity.ApprovalDate = DateTime.UtcNow;
            entity.ApprovalNote = model.ApprovalNote;
            var details = await _detailRepository.GetListByFieldAsync("MasterId", model.Id);
            details = details.ToList() ?? new List<QCInspectionRequestDetail>();
            var Email = "thuypt2@sunhouse.com.vn";//_masterRepository.QueryFirstOrDefault<string>($"SELECT  (CASE WHEN Email <>'' OR Email IS NOT NULL THEN Email ELSE Email2 END) Email FROM dbo.AppUsers WHERE Active=1 AND UserName ='{entity.CreateBy}'");
            var PONumber = string.Join(",", details.Where(n => n.Active).Select(n => n.ordernr).Distinct().ToList());
            var replaceSubject = new Dictionary<string, string>
                {
                    {"{0}",_CurrentUser.UserName },
                    {"{1}",entity.RequestNumber },
                };
            var replacements = new Dictionary<string, string>
                {
                    {"{0}",entity.RequestNumber },
                    {"{1}",PONumber },
                    {"{2}",_CurrentUser.FullName },
                };
            if (model.Status == "CONFIRMED")
            {

                await _masterRepository.UpdateAsync(entity);
                if (Email.IsNotEmpty())
                {
                    var message = await _shEmailTempServices.GetTempateAsync(Email, SendMailType.SendMailConfirm);
                    message.Subject = Utils.ReplaceValues(message.Subject, replaceSubject);
                    message.Body = Utils.ReplaceValues(message.Body, replacements);
                    _rabbitMqService.PublishMessage("sendmail_exchange", "sendmail_routing_key", message);
                }
                return new ServiceResultSuccess("Accepted request successfully");
            }
            else if (model.Status == "REJECT")
            {
                await _masterRepository.UpdateAsync(entity);
                if (Email.IsNotEmpty())
                {
                    var message = await _shEmailTempServices.GetTempateAsync(Email, SendMailType.SendMailReject);
                    message.Subject = Utils.ReplaceValues(message.Subject, replaceSubject);
                    message.Body = Utils.ReplaceValues(message.Body, replacements);
                    _rabbitMqService.PublishMessage("sendmail_exchange", "sendmail_routing_key", message);
                }
                return new ServiceResultSuccess("Rejected request successfully");
            }
            return new ServiceResultError("Update status to failed");

        }
        #endregion
    }
}
