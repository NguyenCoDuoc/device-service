using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace Portal.Domain.Entities
{
    [Description("Bảng nhóm tiêu chí")]
    [Table("QCInspectionGroup")]
    public class QCInspectionGroup
    {
        [Key]
        public string InspectionGroupCode { get; set; }

        public string InspectionGroupName { get; set; }

        public string InspectionGroupNameEN { get; set; }

        public string InspectionGroupNameCN { get; set; }

        public string ShortDescription { get; set; }

        public bool Active { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }


    }
}
