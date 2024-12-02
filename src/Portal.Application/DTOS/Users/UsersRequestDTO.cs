using System.ComponentModel;

namespace DeviceService.Application.DTOS.Users
{
    public class UsersRequestDTO
    {
        [Description("Tài khoản admin")]   
		public string AdminUser { get; set; }
		[Description("Mật khẩu admin")]
		public string AdminPassword { get; set; }
		[Description("RequestId")]
		public string RequestId { get; set; }
        public string RequestNumber { get; set; }
        public string PONumber { get; set; }
        public string CreateBy { get; set; }
        public DateTime RequestDate { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? FollowEmail { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string Country { get; set; } = "en";
	}
}
