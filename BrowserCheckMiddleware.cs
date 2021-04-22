using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
{

    public class BrowserCheckMiddleware
    {
        private RequestDelegate _next;
        public BrowserCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext context)
        {
            String browser = context.Request.Headers["User-Agent"].ToString();
            if (browser.Contains("IE") || browser.Contains("Trident") || browser.Contains("Edg"))
                return context.Response.WriteAsync("Przegladarka nie jest obslugiwana! \n");
            else
                return _next(context);
        }
    }
        public static class BrowserCheckMiddlewareExtensions
        {
            public static IApplicationBuilder UseBrowserCheckMiddleware(this IApplicationBuilder builder)
            { return builder.UseMiddleware<BrowserCheckMiddleware>(); }
        }
    
}
