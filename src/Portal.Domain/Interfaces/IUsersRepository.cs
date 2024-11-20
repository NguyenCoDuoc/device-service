using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces
{
    public interface IUsersRepository : IDapperRepository<Users>
    {
        Task<bool> UserNameExists(string UserName, long id = 0);
        Task<bool> EmailExists(string Email, long id = 0);
    }
}
