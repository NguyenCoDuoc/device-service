using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Domain.Entities
{
    [Table("supplier_contact")]
    public class SupplierContact : CrudFieldEntity<long>
    {
        [Column("supplier_id")]
        public long SupplierId { get; set; }
        public string  Name { get; set; }
        public int Gender { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Manager { get; set; }
        public string Remark { get; set; }
    }
}
