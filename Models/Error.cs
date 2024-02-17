using System.Text.Json.Serialization;

namespace Rainfall.Api.Models
{
	public class Error
	{
		public string Message { get; set; }
		public List<List<ErrorDetail>> Detail { get; set; }
	}

	public class ErrorDetail
	{
		public string PropertyName { get; set; }
		public string Message { get; set; }
	}
}
