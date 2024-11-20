using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;

namespace DeviceService.Domain.Repositories
{
    public class UsersRepository : PortalRepository<Users>, IUsersRepository
    {
        public async Task<bool> UserNameExists(string UserName, long id = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"user_name",UserName },
                {"id",id }
            };
            return await this.ExistsAsync("user_name=@user_name AND id<>@id", param);
        }
        public async Task<bool> EmailExists(string Email, long id = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"email",Email },
                {"id",id }
            };
            return await this.ExistsAsync("email=@email AND id<>@id", param);
        }
    }
}
