using System.Text.Json.Serialization;

namespace Rainfall.Api.Models
{
	public class RainfallReadingResponseDTO
	{
        public RainfallReadingResponseDTO()
        {
			Readings = new List<RainfallReadingDTO>();
		}
		[JsonPropertyName("items")]
		public List<RainfallReadingDTO> Readings { get; set; }
	}
}
