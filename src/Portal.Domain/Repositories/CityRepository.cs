using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Helpers.Attributes;
using Sun.Core.DataAccess.Helpers.Queries;

namespace DeviceService.Domain.Repositories
{
    public class CityRepository : PortalRepository<City>, ICityRepository
    {
        public async Task<IEnumerable<City>> GetByCountryState(long CountryId, long StateId)
        {
            var Columns = SqlHelper.GetPropertyOrColumnsAccess(default(City), CrudFieldType.All, false);
            var selectColumns = Columns.Count != 0 ? string.Join(", ", Columns) : "*";
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"country_id",CountryId },
                {"state_id",StateId }
            };
            return await this.QueryAsync<City>($"select  {selectColumns} from cities where country_id=@country_id and state_id=@state_id ORDER BY name", param);
        }
    }
}
