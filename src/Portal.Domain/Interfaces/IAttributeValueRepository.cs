using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;
using AttributeValue = DeviceService.Domain.Entities.AttributeValue;

namespace DeviceService.Domain.Interfaces;

public interface IAttributeValueRepository : IDapperRepository<AttributeValue>
{
    /// <summary>
    /// Lấy all thuộc tính giá trị
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<AttributeValue>> GetAllAsync();
}