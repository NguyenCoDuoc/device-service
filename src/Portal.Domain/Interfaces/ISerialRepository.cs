using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;
using Serial = DeviceService.Domain.Entities.Serial;

namespace DeviceService.Domain.Interfaces;

public interface ISerialRepository : IDapperRepository<Serial>
{
    Task<List<SerialAttribute>> GetSerialAttributes(long serialId);

    //add serial  attribute
    Task<int> AddSerialAttribute(SerialAttribute serialAttribute, long current_user_id);

    //delete serial  attribute
    Task<int> DeleteSerialAttribute(long id, long current_user_id);
}