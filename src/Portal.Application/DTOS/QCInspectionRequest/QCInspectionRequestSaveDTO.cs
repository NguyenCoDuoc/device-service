﻿using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Portal.Application.DTOS.QCInspectionRequestDetail;

namespace Portal.Application.DTOS.QCInspectionRequest
{
    public class QCInspectionRequestSaveDTO
    {
        public QCInspectionRequestDetailDTOUpdate detail { get; set; }
        public List<QCInspectionRequestDetailResultDTOCreate> detailResults { get; set; }
    }
}
