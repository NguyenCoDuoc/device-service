using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DeviceService.Application.DTOS.Country
{
    public class CountryDTO
	{
        public long? rownumber { get; set; }
        public long? ID { get; set; }
        public string Name { get; set; }

        public string NumericCode { get; set; }

        public string Code { get; set; }

        public string PhoneCode { get; set; }

        public string Capital { get; set; }

        public string Currency { get; set; }

        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        public string Native { get; set; }

        public string Region { get; set; }

        public long? RegionId { get; set; }

        public string Nationality { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
        public long? CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
