using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidBee.Sentry
{
    public static class SentryExtensions
    {
        public static IServiceCollection AddSentry(this IServiceCollection services, Action<SentryOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.AddSingleton<SentryReporter>();
            return services;
        }

        public static IApplicationBuilder UseSentry(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SentryMiddleware>();
        }
    }
}