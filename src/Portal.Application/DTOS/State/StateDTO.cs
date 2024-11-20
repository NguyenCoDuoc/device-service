using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DeviceService.Application.DTOS.State
{
    public class StateDTO
    {
        public long? rownumber { get; set; }
        public long? ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? CountryId { get; set; }
        public string? CountryCode { get; set; }
        public string? FipsCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public long? CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
