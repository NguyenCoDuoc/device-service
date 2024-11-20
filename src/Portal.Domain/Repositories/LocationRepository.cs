using System.Data;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Domain.Repositories
{
    public class LocationRepository : DapperRepository<Location>, ILocationRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public LocationRepository() : base()
        {

        }

    }
}
