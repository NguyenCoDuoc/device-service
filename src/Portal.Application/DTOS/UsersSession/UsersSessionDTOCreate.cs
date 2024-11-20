namespace DeviceService.Application.DTOS.UsersSession
{
    public class UsersSessionDTOCreate : UsersSessionDTO
    {
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
       
        public UsersSessionDTOCreate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
