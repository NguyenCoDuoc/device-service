using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Sun.Core.Share.Helpers.Params;

namespace Portal.Application.DTOS.QCInspectionRequest
{
    public class QCInspectionRequestSearchParam  : SearchParam
	{
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Status { get; set; } = "";
    }
}
