
namespace DeviceService.Application.DTOS.UsersSession
{
    public class UsersSessionDTO
    {
        public long ID { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string IdentityRefreshTokenId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime IssuedTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public bool IsExpired { get { return DateTime.UtcNow >= ExpireTime; } }
        public bool RememberMe { get; set; }
    }
}
