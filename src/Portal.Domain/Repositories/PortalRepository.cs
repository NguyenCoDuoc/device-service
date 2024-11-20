using Sun.Core.DataAccess.Helpers.Queries;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using static Sun.Core.DataAccess.Helpers.Queries.SqlRaw;

namespace DeviceService.Domain.Repositories
{
    public class PortalRepository<T> : DapperRepository<T> where T : class
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public PortalRepository() : base(Dialect.PostgreSQL)
        {
        }
    }
}
