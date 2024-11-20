using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using SerialLocation = DeviceService.Domain.Entities.SerialLocation;

namespace DeviceService.Domain.Repositories
{
    public class SerialLocationRepository : DapperRepository<SerialLocation>, ISerialLocationRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public SerialLocationRepository() : base()
        {

        }
    }
}
