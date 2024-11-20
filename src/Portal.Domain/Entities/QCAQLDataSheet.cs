using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.Entities
{
    [Description("Bảng nhóm tiêu chí")]
    [Table("QCAQLDataSheet")]
    public class QCAQLDataSheet
    {
        [Key]
        public string Id { get; set; }

        public string InspectionLever { get; set; }

        public double LotSizeFrom { get; set; }

        public double LotSizeTo { get; set; }

        public string LetterCode { get; set; }

        public double SampleSize { get; set; }

        public double Inspection { get; set; }

        public double Ac { get; set; }

        public double Re { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public double? P_SampleSize { get; set; }


    }
}
