using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;
using Serial = DeviceService.Domain.Entities.Serial;

namespace DeviceService.Domain.Interfaces;

public interface ISerialRepository : IDapperRepository<Serial>
{
}