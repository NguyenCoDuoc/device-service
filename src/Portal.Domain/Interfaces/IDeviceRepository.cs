using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces;

public interface IDeviceRepository : IDapperRepository<Device>
{
    Task<Device> GetByCodeAsync(string code);

    Task<List<DeviceAttribute>> GetDeviceAttributes(long deviceId);

    //add device  attribute
    Task<int> AddDeviceAttribute(DeviceAttribute deviceAttribute, long current_user_id);

    //delete device  attribute
    Task<int> DeleteDeviceAttribute(long id, long current_user_id);
}