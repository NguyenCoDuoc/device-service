using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities
{
    [Description("Bảng thông tin phiếu yêu cầu")]
    [Table("QCInspectionRequestMaster")]
    public class QCInspectionRequestMaster
    {
        [Key]    
		public string Id { get; set; }
		public string cmp_wwn { get; set; }

		public string cmp_name { get; set; }

		public string crdcode { get; set; }
		[Description("Số yêu cầu")]
        public string RequestNumber { get; set; }
        [Description("Ngày yêu cầu")]
        public DateTime RequestDate { get; set; }
        [Description("Trạng thái")]
        public string Status { get; set; }
        [Description("Người theo dõi")]
        public string FollowBy { get; set; }
        public bool Approval { get; set; }
        public string ApprovalBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string ApprovalNote { get; set; }
        public bool IsSend { get; set; }
        public string SendBy { get; set; }
        public string SendNote { get; set; }
        public DateTime SendDate { get; set; }
        public bool Active { get; set; }
        public string Syslog { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string ModifyBy { get; set; }
		public DateTime? ModifyDate { get; set; }

	}
}
