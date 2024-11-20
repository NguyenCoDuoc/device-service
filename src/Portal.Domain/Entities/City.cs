using Sun.Core.DataAccess.Helpers.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceService.Domain.Entities
{
    // Bảng thành phố/quận/huyện

    [Table("cities")]
    public class City : CrudFieldEntity<long>
    {
        [Description("Mã của thành phố")]
        [Column("code")]
        public string Code { get; set; }
        [Description("Tên của thành phố")]
        [Column("name")]
        public string Name { get; set; }

        [Description("ID của bang hoặc tiểu bang")]
        [Column("state_id")]
        public long StateId { get; set; }

        [Description("Mã của bang hoặc tiểu bang")]
        [Column("state_code")]
        public string StateCode { get; set; }

        [Description("ID quốc gia")]
        [Column("country_id")]
        public long CountryId { get; set; }

        [Description("Mã quốc gia dạng chữ (2 ký tự)")]
        [Column("country_code")]
        public string CountryCode { get; set; }

        [Description("Vĩ độ của thành phố")]
        [Column("latitude")]
        public decimal Latitude { get; set; }

        [Description("Kinh độ của thành phố")]
        [Column("longitude")]
        public decimal Longitude { get; set; }
    }
}
