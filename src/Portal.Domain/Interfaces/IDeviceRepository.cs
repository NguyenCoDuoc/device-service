using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces;

public interface IDeviceRepository : IDapperRepository<Device>
{
    Task<Device> GetByCodeAsync(string code);
}