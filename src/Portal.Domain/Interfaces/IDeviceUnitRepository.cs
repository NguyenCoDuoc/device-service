using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces;

public interface IDeviceUnitRepository : IDapperRepository<DeviceUnit>
{
}