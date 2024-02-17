using System.Text.Json.Serialization;

namespace Rainfall.Api.Models
{
	public class RainfallReadingResponse
	{
        public RainfallReadingResponse()
        {
			Readings = new List<RainfallReading>();
		}
		[JsonPropertyName("items")]
		public List<RainfallReading> Readings { get; set; }
	}
}
