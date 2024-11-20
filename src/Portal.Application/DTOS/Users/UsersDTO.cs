using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DeviceService.Application.DTOS.Users
{
    public class  UsersDTO
    {
        public long rownumber { get; set; }
        public long ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public int? Gender { get; set; }
        public int? Status { get; set; }
        public int? CountLoginFail { get; set; }
        public bool IsAdmin { get; set; }
        public long? Parent { get; set; }
        public string? Description { get; set; }
		public string cmp_wwn { get; set; }
		public string? Country { get; set; }
		public string? State { get; set; }
		public string? City { get; set; }
		public string? PostalCode { get; set; }
		public string? VatNumber { get; set; }
		public string? Currency { get; set; }
		public string? Fax { get; set; }
		public string? Website { get; set; }
	}
}
