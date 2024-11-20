using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Portal.Domain.Entities
{
    /// <summary>
    /// Inspection Dashboard
    /// </summary>
    public class InspectionDashboard
    {
        public long wait { get; set; }
        public long done { get; set; }
        public long processing { get; set; }
        public long completed { get; set; }
        public long shreject { get; set; }
        public long confirm { get; set; }
    }
}
