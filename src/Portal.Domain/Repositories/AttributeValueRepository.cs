using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using AttributeValue = DeviceService.Domain.Entities.AttributeValue;

namespace DeviceService.Domain.Repositories
{
    public class AttributeValueRepository : DapperRepository<AttributeValue>, IAttributeValueRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public AttributeValueRepository() : base()
        {

        }

        public async Task<IEnumerable<AttributeValue>> GetAllAsync()
        {
            return await QueryAsync<AttributeValue>("SELECT * FROM attribute_value WHERE is_deleted = false");
        }
    }
}
