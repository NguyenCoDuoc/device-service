using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Domain.Repositories
{
    public class DeviceRepository : DapperRepository<Device>, IDeviceRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public DeviceRepository() : base()
        {
           

        }
        public async Task<Device> GetByCodeAsync(string code)
        {
            {
                var query = @"
                    SELECT * FROM device 
                    WHERE Code = @Code";
                var parameters = new Dictionary<string, object>
                {
                    { "@Code", code }
                };

                return await QueryFirstOrDefaultAsync<Device?>(query, parameters);
            }
        }

    }
}
