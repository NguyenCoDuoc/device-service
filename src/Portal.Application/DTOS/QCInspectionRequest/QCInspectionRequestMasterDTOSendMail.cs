using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Application.DTOS.QCInspectionRequest
{
    public class QCInspectionRequestMasterDTOSendMail
    {
        [Description("Tài khoản admin")]
        public string AdminUser { get; set; }
        [Description("Mật khẩu admin")]
        public string AdminPassword { get; set; }
        public string? RequestNumber { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? PONumber { get; set; }
        public string? ItemCode { get; set; }
        public int? TypeSendMail { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

    }
}
