using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DeviceService.Application.DTOS.City
{
    public class CityDTO
    {
        public long rownumber { get; set; }
        public long ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public long StateId { get; set; }

        public string StateCode { get; set; }

        public long CountryId { get; set; }

        public string CountryCode { get; set; }

        public decimal Latitude { get; set; }
    }
}
