using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rainfall.Api.Enums;
using Rainfall.Api.Models;

namespace Rainfall.Api.ActionFilters
{
    public class CustomBadRequestAttribute : ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext context)
        {
			if (!context.ModelState.IsValid)
			{
				context.Result = new BadRequestObjectResult(new
				{
					message = "Invalid request. Please check the request parameters."
				});
			}

			IDictionary<string, object> actionArguments = context.ActionArguments;

			var errorDetails = new List<ErrorDetail>();

			if (actionArguments.ContainsKey("stationId"))
			{
				if (string.IsNullOrEmpty(actionArguments["stationId"].ToString()))
					errorDetails.Add(new ErrorDetail
					{
						PropertyName = "stationId",
						Message = "Must not be empty"
					});
			}

			if (actionArguments.ContainsKey("count"))
			{
				var countStr = actionArguments["count"].ToString();
				if (!string.IsNullOrEmpty(countStr))
				{
					int.TryParse(countStr, out int count);

					if (count < 1 || count > 100)
						errorDetails.Add(new ErrorDetail
						{
							PropertyName = "count",
							Message = "Count parameter must be between 1 and 100"
						});
				}
			}

			if(errorDetails.Any())
				context.Result = new BadRequestObjectResult(new Error
				{
					Message = "Invalid request. Please check the request parameters.",
					Detail = new List<List<ErrorDetail>> { errorDetails }
				});
		}
	}
}
