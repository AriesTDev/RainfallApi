using System.Text.Json.Serialization;

namespace Rainfall.Api.Models
{
	public class RainfallReading
	{
		public DateTime DateMeasured { get; set; }
		public decimal AmountMeasured { get; set; }
	}
}
