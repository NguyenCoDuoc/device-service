using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;

namespace DeviceService.Domain.Repositories
{
    public class SupplierRepository : PortalRepository<Supplier>, ISupplierRepository
	{
        public async Task<bool> NameExists(string Name, long id = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"name",Name },
                {"id",id }
            };
            return await this.ExistsAsync("name=@name AND id<>@id", param);
        }
        public async Task<bool> CodeExists(string Code, long id = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"Code",Code },
                {"id",id }
            };
            return await this.ExistsAsync("code=@code AND id<>@id", param);
        }
    }
}
