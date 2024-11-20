namespace DeviceService.Application.DTOS.Users
{
    public class UsersDTOUpdate : UsersDTO
    {
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public UsersDTOUpdate()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
