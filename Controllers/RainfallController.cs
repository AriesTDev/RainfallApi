using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.ActionFilters;
using Rainfall.Api.Models;
using Rainfall.Api.Repositories;

namespace Rainfall.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RainfallController : ControllerBase
	{

		private readonly IRainfallRepository _rainfallRepository;
		public RainfallController(IRainfallRepository rainfallRepository)
		{
			_rainfallRepository = rainfallRepository;
		}

		[HttpGet("id/{stationId}/readings")]
		[CustomBadRequest]
		public async Task<IActionResult> GetRainfallByStationId([FromRoute]string stationId, [FromQuery] int count)
		{
			try
			{
				var result = await _rainfallRepository.GetRainfallByStationId(stationId, count);

				switch (result.StatusCode)
				{
					case Enums.StatusCode.Success:
						return Ok(result.Result);
					case Enums.StatusCode.NotFound:
						return NotFound(new Error { Message = "No readings found for the specified stationId", Detail = new List<List<ErrorDetail>>() });
					case Enums.StatusCode.Error:
						return NotFound(new Error { Message = "AnInternal server error", Detail = new List<List<ErrorDetail>>() });
					default:
						return BadRequest(new Error { Message = result.Message, Detail = new List<List<ErrorDetail>>() });
				}

			}
			catch (HttpRequestException ex)
			{
				return StatusCode(500, new { Message = "Internal server error", Detail = ex.Message });
			}
		}
	}
}