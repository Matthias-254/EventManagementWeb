using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EventManagementWeb.Services
{
    public class MyMiddleWare
    {
        private readonly RequestDelegate _next;

        public MyMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.TryGetValue("UserPreference", out var userPreference))
            {
                httpContext.Items["UserPreference"] = userPreference;
            }
            else
            {
                httpContext.Items["UserPreference"] = "DefaultPreference";
            }

            await _next(httpContext);
        }
    }

    public static class MyMiddleWareExtensions
    {
        public static IApplicationBuilder UseMyMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleWare>();
        }
    }
}
