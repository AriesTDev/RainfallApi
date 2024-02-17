using Microsoft.AspNetCore.Http;
using Rainfall.Api.Enums;

namespace Rainfall.Api.Models
{
	public class BaseResponse<T>
	{
		public StatusCode StatusCode { get; set; }
		public string Message { get; set; }
		public T Result { get; set; }
	}
}
