using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;
using Attribute = DeviceService.Domain.Entities.Attribute;

namespace DeviceService.Domain.Interfaces;

public interface IAttributeRepository : IDapperRepository<Attribute>
{
    /// <summary>
    /// Lấy all thuộc tính
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Attribute>> GetAllAsync();
}