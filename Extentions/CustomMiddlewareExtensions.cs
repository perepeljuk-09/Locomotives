using Locomotives.API.Middlewares;

namespace Locomotives.API.Extentions
{
	public static class CustomMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomLogError( this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomLogErrorMiddleware>();
		}
	}
}
