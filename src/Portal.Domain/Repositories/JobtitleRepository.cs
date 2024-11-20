using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using Jobtitle = DeviceService.Domain.Entities.Jobtitle;

namespace DeviceService.Domain.Repositories
{
    public class JobtitleRepository : DapperRepository<Jobtitle>, IJobtitleRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public JobtitleRepository() : base()
        {

        }
    }
}
