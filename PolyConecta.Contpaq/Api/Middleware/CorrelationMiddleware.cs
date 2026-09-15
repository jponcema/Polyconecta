using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contpaq.Bridge.Api.Middleware
{
    public class CorrelationMiddleware
    {
        private const string CorrelationHeader = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(CorrelationHeader, out var correlationId) || string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            context.Items[CorrelationHeader] = correlationId.ToString();
            context.Response.Headers[CorrelationHeader] = correlationId.ToString();

            await _next(context);
        }
    }
}
