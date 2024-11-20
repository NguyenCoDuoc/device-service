using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using Attribute = DeviceService.Domain.Entities.Attribute;

namespace DeviceService.Domain.Repositories
{
    public class AttributeRepository : DapperRepository<Attribute>, IAttributeRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public AttributeRepository() : base()
        {

        }

        public async Task<IEnumerable<Attribute>> GetAllAsync()
        {
            return await QueryAsync<Attribute>("SELECT * FROM attribute WHERE is_deleted = 0");
        }
    }
}
