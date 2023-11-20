//namespace BidLand.Web.Utilities.Middlewares
//{
//	public class ErrorHandling
//	{
//		private readonly RequestDelegate _next;
//		private readonly ILogingService _loggingService;
//		public ErrorHandling(RequestDelegate next, ILogingService loggingService)
//		{
//			_next = next;
//			_loggingService = loggingService;
//		}

//		public async Task Invoke(HttpContext context)
//		{
//			try
//			{

//				await _next(context);
//			}
//			catch (Exception ex)
//			{
//				var controllerName = context.GetRouteValue("controller")?.ToString();
//				var actionName = context.GetRouteValue("action")?.ToString();

//				await _loggingService.LogError($"ROUTE:{controllerName}/{actionName} || ERROR:{ex}", "ResponseLog");
//				context.Response.Redirect("/Home/ErrorVisitor");
//			}
//		}
//	}

//	// Extension method used to add the middleware to the HTTP request pipeline.
//	public static class ErrorHandlingExtensions
//	{
//		public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
//		{
//			return builder.UseMiddleware<ErrorHandling>();
//		}
//	}
//}
//}
