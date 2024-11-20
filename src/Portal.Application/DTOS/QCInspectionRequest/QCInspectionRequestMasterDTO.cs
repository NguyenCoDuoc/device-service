using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Portal.Application.DTOS.QCInspectionRequest
{
    public class QCInspectionRequestMasterDTO
    {
        public string Id { get; set; }
        public string MasterId { get; set; }
		public string cmp_wwn { get; set; }
		public string cmp_name { get; set; }
		public string RequestNumber { get; set; }
        public DateTime RequestDate { get; set; }
		public string PO { get; set; }
		public DateTime NeedDate { get; set; }
		public string Status { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string FollowBy { get; set; }
        public bool Approval { get; set; }
        public string ApprovalBy { get; set; }
      
        public DateTime ApprovalDate { get; set; }
        public string ApprovalNote { get; set; }
    }
}
