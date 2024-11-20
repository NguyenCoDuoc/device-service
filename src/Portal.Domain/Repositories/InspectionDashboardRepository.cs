using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Portal.Domain.Repositories
{
    public class InspectionDashboardRepository : ExtAppRepository<InspectionDashboard>, IInspectionDashboardRepository
    {

        public async Task<InspectionDashboard> GetStaticStatus(string cmp, int month, int year, bool isViewRequest)
        {
            var sql = "EXEC [dbo].[Proc_QC_QCInspection_Dashboard] @cmp, @month, @year, @isViewRequest";

            var parameters = new Dictionary<string, object>
                {
                    { "@cmp", cmp },
                    { "@month", month },
                    { "@year", year },
                    { "@isViewRequest", isViewRequest }
                };

            var result = await QueryFirstOrDefaultAsync<InspectionDashboard>(sql, parameters);

            return result ?? new InspectionDashboard();
        }
    }
}
