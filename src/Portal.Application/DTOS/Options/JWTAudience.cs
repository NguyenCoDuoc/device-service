
namespace DeviceService.Application.DTOS.Options
{
    public class JWTAudience
    {
        public string Secret { get; set; }
        public string Key { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int ExpiresTimeToken { get; set; }
    }
}
