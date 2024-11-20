using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces
{
    public interface ICityRepository : IDapperRepository<City>
    {
        Task<IEnumerable<City>> GetByCountryState(long CountryId, long StateId);
    }
}
