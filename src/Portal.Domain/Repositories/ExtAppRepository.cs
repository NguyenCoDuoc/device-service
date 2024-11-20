using Sun.Core.DataAccess.Helpers.Queries;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using static Sun.Core.DataAccess.Helpers.Queries.SqlRaw;

namespace Portal.Domain.Repositories
{
    public class ExtAppRepository<T> : DapperRepository<T> where T : class
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("ExtAppMSSQL");
        }
        public ExtAppRepository() : base(Dialect.SQLServer)
        {
        }
    }
}
