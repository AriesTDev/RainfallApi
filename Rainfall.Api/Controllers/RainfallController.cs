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

		/// <summary>
		/// Get rainfall readings by station Id
		/// </summary>
		/// <param name="stationId">The id of the reading station</param>
		/// <param name="count">The number of readings to return</param>
		/// <returns> Retrieve the latest readings for the specified stationId</returns>
		/// <remarks>
		/// The API returns the following status codes:
		/// - 200: A list of rainfall readings successfully retrieved
		/// - 400: Invalid request 
		/// - 404: No readings found for the specified stationId
		/// - 500: Internal server error
		/// </remarks>
		[HttpGet("id/{stationId}/readings")]
		[CustomBadRequest]
		[ProducesResponseType(typeof(RainfallReadingResponse), 200)]
		[ProducesResponseType(typeof(Error), 400)]
		[ProducesResponseType(typeof(Error), 404)]
		[ProducesResponseType(typeof(Error), 500)]
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
						return NotFound(new Error { Message = "Internal server error", Detail = new List<List<ErrorDetail>>() });
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