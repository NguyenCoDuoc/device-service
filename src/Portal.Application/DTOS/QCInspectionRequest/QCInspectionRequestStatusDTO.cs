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
    public class QCInspectionRequestStatusDTO
	{
        public string Id { get; set; }
        public string Status { get; set; }
		public string? ApprovalNote { get; set; }
    }
}
