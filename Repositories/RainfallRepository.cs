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

		public async Task<BaseResponse<RainfallReadingResponse>> GetRainfallByStationId(string stationId, int count)
		{
			try
			{
				var limitFilter = count == 0 ? "" : $"&_limit={count}";
				var uri = new Uri($"{_configuration["API:RainfallAPI"]}/id/stations/{stationId}/readings?_sorted&parameter=rainfall{limitFilter}");

				var response = await _httpClient.GetAsync(uri);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync();

				var rainfallReadingResponse = JsonSerializer.Deserialize<RainfallReadingResponseDTO>(responseBody);

				if(rainfallReadingResponse == null || !rainfallReadingResponse.Readings.Any())
					return new BaseResponse<RainfallReadingResponse>
					{
						StatusCode = StatusCode.NotFound,
						Result = new RainfallReadingResponse()
					};

				var rainfallReadingResult = new RainfallReadingResponse
				{
					Readings = rainfallReadingResponse.Readings.Select(x => new RainfallReading
					{
						AmountMeasured = x.AmountMeasured,
						DateMeasured = x.DateMeasured
					}).ToList()
				};

				return new BaseResponse<RainfallReadingResponse>
				{
					StatusCode = StatusCode.Success,
					Result = rainfallReadingResult
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<RainfallReadingResponse>
				{
					StatusCode = StatusCode.Error,
					Message = "An error occurred while fetching rainfall data.",
				};
			}
		}
	}
}
