using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.Models;

namespace Rainfall.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RainfallController : ControllerBase
	{
		private static readonly Dictionary<int, List<RainfallReading>> StationReadings = new Dictionary<int, List<RainfallReading>>
		{
			{ 1, new List<RainfallReading> { new RainfallReading { DateMeasured = DateTime.Now.AddDays(-1), AmountMeasured = 10.5m } } },
			{ 2, new List<RainfallReading> { new RainfallReading { DateMeasured = DateTime.Now.AddDays(-1), AmountMeasured = 5.2m } } }
		};

		private readonly ILogger<RainfallController> _logger;

		public RainfallController(ILogger<RainfallController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{stationId}")]
		public IActionResult Get(int stationId)
		{
			if (!StationReadings.ContainsKey(stationId))
			{
				return NotFound(new { Message = "No readings found for the specified stationId" });
			}

			var readings = StationReadings[stationId];
			return Ok(new RainfallReadingResponse { Readings = readings });
		}
	}
}