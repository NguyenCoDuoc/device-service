namespace DeviceService.Application.DTOS.Users
{
    public class UpdateProfileDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public int? Gender { get; set; }
        public string? Description { get; set; }
    }
}
