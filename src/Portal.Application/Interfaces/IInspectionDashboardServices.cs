using Sun.Core.Share.Helpers.Results;

namespace Portal.Application.Interfaces
{
    public interface IInspectionDashboardServices
    {
        /// <summary>
        /// Dashboard sum các trạng thái InspectionDetail
        /// </summary>
        /// DUOCNC 02102024
        Task<ServiceResult> GetStaticStatus(string cmp, int month, int year, bool isViewRequest);

    }
}
