using Microsoft.AspNetCore.Http;

namespace Rainfall.Api.Models
{
	public class BaseResponse<T>
	{
		public string Message { get; set; }
		public T Result { get; set; }
	}
}
