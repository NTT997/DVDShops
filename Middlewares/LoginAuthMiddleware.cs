using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DVDShops.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string request = httpContext.Request.Path.ToString();
            if (request.StartsWith("/admin", StringComparison.InvariantCultureIgnoreCase))
            {
                if (httpContext.Session.GetString("role") != "admin")
                {
                    httpContext.Response.Redirect("/login");
                    return;
                }
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginAuthExtensions
    {
        public static IApplicationBuilder UseLoginAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginAuthMiddleware>();
        }
    }
}
