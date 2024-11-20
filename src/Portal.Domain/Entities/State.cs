using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Domain.Entities
{
    // Bảng tỉnh thành  

    [Table("states")]
    public class State : CrudFieldEntity<long>
    {
        [Description("Tên của bang hoặc tiểu bang")]
        [Column("name")]
        public string Name { get; set; }

        [Description("ID quốc gia")]
        [Column("country_id")]
        public long CountryId { get; set; }

        [Description("Mã quốc gia dạng chữ (2 ký tự)")]
        [Column("country_code")]
        public string CountryCode { get; set; }

        [Description("Mã FIPS")]
        [Column("fips_code")]
        public string FipsCode { get; set; }

        [Description("Mã bang hoặc tiểu bang")]
        [Column("code")]
        public string Code { get; set; }

        [Description("Vĩ độ")]
        [Column("latitude")]
        public decimal? Latitude { get; set; }

        [Description("Kinh độ")]
        [Column("longitude")]
        public decimal? Longitude { get; set; }
    }
}
