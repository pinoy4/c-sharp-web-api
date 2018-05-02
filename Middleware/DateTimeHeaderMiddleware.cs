using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MWTest.Middleware
{
    public class DateTimeHeaderMiddleware
    {
        private RequestDelegate _next;

        public DateTimeHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
            context.Response.Headers.Add("Request-DateTime", DateTime.Now.ToString());
        }
    }
}
