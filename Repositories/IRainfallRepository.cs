using Rainfall.Api.Models;

namespace Rainfall.Api.Repositories
{
	public interface IRainfallRepository
	{
		Task<BaseResponse<RainfallReadingResponse>> GetRainfallByStationId(string stationId);
	}
}
