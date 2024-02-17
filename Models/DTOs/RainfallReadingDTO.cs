using System.Text.Json.Serialization;

namespace Rainfall.Api.Models
{
	public class RainfallReadingDTO
	{
		[JsonPropertyName("dateTime")]
		public DateTime DateMeasured { get; set; }
		[JsonPropertyName("value")]
		public decimal AmountMeasured { get; set; }
	}
}
