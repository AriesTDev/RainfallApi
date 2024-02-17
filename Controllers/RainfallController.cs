using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.Repositories;

namespace Rainfall.Api.Controllers
{
	[ApiController]
	[Route("[controller]/v1")]
	public class RainfallController : ControllerBase
	{

		private readonly IRainfallRepository _rainfallRepository;
		public RainfallController(IRainfallRepository rainfallRepository)
		{
			_rainfallRepository = rainfallRepository;
		}

		[HttpGet("{stationId}")]
		public async Task<IActionResult> GetRainfallByStationId(string stationId)
		{
			try
			{
				var result = await _rainfallRepository.GetRainfallByStationId(stationId);

				switch (result.StatusCode)
				{
					case Enums.StatusCode.Success:
						return Ok(result.Result);
					default:
						return BadRequest(result);
				}

			}
			catch (HttpRequestException ex)
			{
				return StatusCode(500, new { Message = "An error occurred while fetching rainfall data.", Detail = ex.Message });
			}
		}
	}
}