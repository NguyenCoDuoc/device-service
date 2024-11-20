using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;
using Jobtitle = DeviceService.Domain.Entities.Jobtitle;

namespace DeviceService.Domain.Interfaces;

public interface IJobtitleRepository : IDapperRepository<Jobtitle>
{
}