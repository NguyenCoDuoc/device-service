using Sun.Core.DataAccess.Helpers.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceService.Domain.Entities
{
    [Table("users")]
    public class Users : CrudFieldEntity<long>
    {
        [Description("Tên người dùng")]
        [Column("full_name")]
        public string FullName { get; set; }
        [Description("Email")]
        public string Email { get; set; }
       
        [Description("Tên đăng nhập")]
        [Column("user_name")]
        public string UserName { get; set; }
       
        [Description("Mật khẩu")]
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Description("Số điện thoại")]
        public string Phone { get; set; }
        [Description("Địa chỉ")]
        public string Address { get; set; }
        [Description("Avatar")]
        public string Avatar { get; set; }
        [Description("Giới tính")]
        public int? Gender { get; set; }
        [Description("Trạng thái")]
        public int? Status { get; set; }
        [Description("Số lần đăng nhập sai")]
        [Column("count_login_fail")]
        public int? CountLoginFail { get; set; }
        [Description("Quản trị")]
        [Column("is_admin")]
        public bool IsAdmin { get; set; }
        [Description("Cha con")]
        public long? Parent { get; set; }
        [Description("Mô tả")]
        public string Description { get; set; }
		[Description("Mã nhà cung cấp")]
		public string cmp_wwn { get; set; }
	}
}
