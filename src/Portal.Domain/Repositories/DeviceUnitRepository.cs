using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;

namespace DeviceService.Domain.Repositories
{
    public class DeviceUnitRepository : DapperRepository<DeviceUnit>, IDeviceUnitRepository
    {
        public DeviceUnitRepository() : base()
        {

        }
    }
}
