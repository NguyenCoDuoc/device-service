using System.ComponentModel;

namespace DeviceService.Application.Helpers.Enums
{
    public static class EnumCommon
    {
        public enum StatusUsers
        {
            [Description("Vô hiệu hóa")]
            Locked = 0,
            [Description("Hiệu lực")]
            Active = 1,
        }
        public enum GenderType
        {
            [Description("Nam")]
            Male = 1,
            [Description("Nữ")]
            Female = 2,
            [Description("Khác")]
            Other = 3,
            [Description("Không xác định")]
            Unspecified = 0
        }
        public enum SendMailType
        {
            [Description("Mail gửi yêu cầu mới")]
            SendMailRequest = 1,
            [Description("Mail gửi yêu cầu cùng với tài khoản mới")]
            SendMailRequestAccount = 2,
            [Description("Mail gửi xác nhận")]
            SendMailConfirm = 3,
            [Description("Mail NCC từ chối")]
            SendMailReject = 4,
            [Description("Mail NCC gửi kết quả")]
            SendMaillResult = 5
        }
    }
}
