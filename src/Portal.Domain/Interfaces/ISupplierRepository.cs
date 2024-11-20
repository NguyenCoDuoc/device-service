using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces
{
    public interface ISupplierRepository : IDapperRepository<Supplier>
    {
        Task<bool> NameExists(string Name, long id = 0);
        Task<bool> CodeExists(string Code, long id = 0);
    }
}
