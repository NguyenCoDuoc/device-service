namespace DeviceService.Application.DTOS.Users
{
    public class UsersDTOCreate : UsersDTO
    {
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public UsersDTOCreate() {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
