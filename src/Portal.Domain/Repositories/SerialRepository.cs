using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using Serial = DeviceService.Domain.Entities.Serial;

namespace DeviceService.Domain.Repositories
{
    public class SerialRepository : DapperRepository<Serial>, ISerialRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public SerialRepository() : base()
        {

        }
    }
}
