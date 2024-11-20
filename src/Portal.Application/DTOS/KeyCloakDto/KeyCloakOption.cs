namespace DeviceService.Application.DTOS
{
    public class KeyCloakOption
    {
        public string? Url { get; set; }
        public string? Realm { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? PasswordDefault { get; set; }
    }
}