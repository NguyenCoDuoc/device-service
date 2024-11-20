using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace Portal.Domain.Entities
{
    [Description("Bảng tiêu chí kiểm hàng theo mã hàng")]
    [Table("QCInspectionItemCode")]
    public class QCInspectionItemCode 
    {
        [Key]
        public long Id { get; set; }

        public string ItemCode { get; set; }

        public string InspectionGroupCode { get; set; }

        public string InspectionGroupName { get; set; }

        public string InspectionItemCode { get; set; }

        public string InspectionItemName { get; set; }

        public string InspectionItemNameEN { get; set; }

        public string InspectionItemNameCN { get; set; }

        public string InspectionDescription { get; set; }

        public string InspectionDescriptionEN { get; set; }

        public string InspectionDescriptionCN { get; set; }

        public string InspectionMethod { get; set; }
        public string InspectionMethodEN { get; set; }
        public string InspectionMethodCN { get; set; }

        public bool InspectionCTQ { get; set; }

        public string StandardValue { get; set; }

        public double NormalInspection { get; set; }

        public bool Active { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string MasterId { get; set; }

    }
}
