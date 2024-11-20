using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces;

public interface IDeviceTypeRepository : IDapperRepository<DeviceType>
{
    Task<List<DeviceTypeAttribute>> GetDeviceTypeAttributes(long deviceTypeId);

    //add device type attribute
    Task<int> AddDeviceTypeAttribute(DeviceTypeAttribute deviceTypeAttribute, long current_user_id);

    //delete device type attribute
    Task<int> DeleteDeviceTypeAttribute(long id, long current_user_id);
}