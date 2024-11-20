using Portal.Domain.Entities;
using Portal.Domain.Repositories;
using Sun.Core.DataAccess.Interfaces;

namespace Portal.Domain.Interfaces
{
    public interface IInspectionDashboardRepository : IDapperRepository<InspectionDashboard>
    {
        /// <summary>
        /// Dashboard sum các trạng thái InspectionDetail
        /// </summary>
        /// DUOCNC 02102024
        Task<InspectionDashboard> GetStaticStatus(string cmp, int month, int year, bool isViewRequest);
    }
}
