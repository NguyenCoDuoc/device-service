using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sun.Core.DataAccess.Helpers.Attributes;

namespace DeviceService.Domain.Entities
{
    [Description("Bảng thông tin Email mẫu")]
    [Table("email_template")]
    public class SHEmailTemp : CrudFieldEntity<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        [Column("html_body")]
        public string HtmlBody { get; set; }
        public string Description { get; set; }
    }
}
