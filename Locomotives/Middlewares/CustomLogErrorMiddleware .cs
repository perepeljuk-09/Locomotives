using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Net;

namespace Locomotives.API.Middlewares
{
	public class CustomLogErrorMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomLogErrorMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{

			try
			{
				Console.WriteLine("start middleware");
				await _next(httpContext);
				Console.WriteLine("finish middleware");
			}
			catch(Exception exception)
			{

				httpContext.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
				httpContext.Response.ContentType = "text/html";
				var err = httpContext.Features.Get<IExceptionHandlerFeature>();
				Console.WriteLine("Error >>>>" + exception.Message);

				await httpContext.Response.WriteAsync(exception.Message);
			}
		}
	}
}
