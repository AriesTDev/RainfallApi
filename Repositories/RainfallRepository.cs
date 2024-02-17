using Rainfall.Api.Enums;
using Rainfall.Api.Models;
using System.Text.Json;

namespace Rainfall.Api.Repositories
{
	public class RainfallRepository : IRainfallRepository
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;
        public RainfallRepository(HttpClient httpClient, IConfiguration configuration)
		{
			_configuration = configuration;
			_httpClient = httpClient;
		}

		public async Task<BaseResponse<RainfallReadingResponse>> GetRainfallByStationId(string stationId)
		{
			try
			{
				var uri = new Uri($"{_configuration["API:RainfallAPI"]}/id/stations/{stationId}/readings?_limit=1&_sorted&parameter=rainfall");

				var response = await _httpClient.GetAsync(uri);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync();

				//todo: mapping of rainfall api response

				var rainfallReading = JsonSerializer.Deserialize<RainfallReadingResponse>(responseBody);

				//todo: check if no readings found

				return new BaseResponse<RainfallReadingResponse>
				{
					StatusCode = StatusCode.Success,
					Result = rainfallReading ?? new RainfallReadingResponse()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<RainfallReadingResponse>
				{
					StatusCode = StatusCode.Error,
					Message = "An error occurred while fetching rainfall data.",
				};
				//return new RainfallReadingResponse(); // StatusCode(500, new ErrorResponse { Message = "An error occurred while fetching rainfall data.", Detail = ex.Message });
			}
		}
	}
}
