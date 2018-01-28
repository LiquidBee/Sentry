using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiquidBee.Sentry
{
    public class SentryMiddleware
    {
        private readonly SentryReporter _sentryReporter;
        private readonly RequestDelegate _next;

        public SentryMiddleware(RequestDelegate next, SentryReporter sentryReporter)
        {
            _sentryReporter = sentryReporter;
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await _sentryReporter.CaptureAsync(exception);
                throw;
            }
        }
    }
}