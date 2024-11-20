using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;
using SerialLocation = DeviceService.Domain.Entities.SerialLocation;

namespace DeviceService.Domain.Interfaces;

public interface ISerialLocationRepository : IDapperRepository<SerialLocation>
{
}